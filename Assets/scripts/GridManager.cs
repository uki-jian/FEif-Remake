using UnityEngine;
using System.Collections.Generic;


public class GridManager : MonoBehaviour
{
    public MeshRenderer meshFilter;
    public Camera my_camera;
    public JsonManager jsonParser;

    public int[,] unitMap;//是否有人站在某个格子上,每个格子上人物的编号（无人则为-1）

    Vector3 scale;      //世界尺寸
    public int num_w; //长度格数
    public int num_h; //高度格数
    public float base_altitude = 0.0f;  //y轴基准高度
    public float altitude_radio = 1f; //y轴拉伸系数
    float[] delta_pos;
    float cellSize;
    public Vector3 startPos;  //起点坐标
    public int[,] arrayCell;  //网格数组，存储网格内容
    public TextMesh[,] arrayTextMesh;      //存储网格textmesh，具体是用来表现
    public GridUnit[,] grids;    //每个网格的属性
    public GridUnit currentGrid;   //正被选中的格子

    public GameObject gridPatternChoose;
    public GameObject gridPatternAttack;
    public GameObject gridPatternDanger;
    public GameObject gridPatternFullDanger;

    //List<Pos> movePos;
    //List<Pos> attackPos;

    void Start()
    {
        //new WaitForSeconds(3);

        startPos = meshFilter.bounds.min;
        scale = meshFilter.bounds.size;
        float width = scale.x, height = scale.z;
        cellSize = 2;

        num_w = jsonParser.gridProperties.width;// (int)(width/cellSize);
        num_h = jsonParser.gridProperties.height;// (int)(height/cellSize);
        delta_pos = new float[2];
        delta_pos[0] = 1.0f / num_w;
        delta_pos[1] = 1.0f / num_h;

        //Debug.Log("width：" + width + " height: " + height);
        //Debug.Log("num_w：" + num_w + " num_h: " + num_h);
        //Debug.Log("cellSize：" + cellSize);
        //Debug.Log("delta_pos：" + delta_pos[0]);

        arrayCell = new int[num_w, num_h];
        arrayTextMesh = new TextMesh[num_w, num_h];


        unitMap = new int[num_w, num_h];
        for (int i = 0; i < num_h; i++)
        {
            for (int j = 0; j < num_w; j++)
            {
                unitMap[i, j] = -1;
            }
        }
        grids = new GridUnit[num_w, num_h];   //建立每个网格的属性

        //Instantiate(gridImg, transform.position, Quaternion.identity);
        //画线
        for (int j = 0; j < num_h; j++)
        {
            for (int i = 0; i < num_w; i++)
            {
                grids[i, j] = new GridUnit(jsonParser.gridProperties.gridProperties[j * num_w + i],
                    SetGridPattern(gridPatternChoose, GetWorldPosCenter(i, j)),
                    SetGridPattern(gridPatternAttack, GetWorldPosCenter(i, j)),
                    SetGridPattern(gridPatternDanger, GetWorldPosCenter(i, j)),
                    SetGridPattern(gridPatternFullDanger, GetWorldPosCenter(i, j)));

                grids[i, j].SetPos(i, j);
                arrayTextMesh[i, j] = SetText(arrayCell[i, j].ToString(), GetWorldPosCenter(i, j), Color.red);  //写数字
                Debug.DrawLine(GetLeftUpWorldPos(i, j), GetRightUpWorldPos(i, j), Color.black, 100f);
                Debug.DrawLine(GetLeftUpWorldPos(i, j), GetLeftDownWorldPos(i, j), Color.black, 100f);
                //grids[i, j].SetPattern(SetGridPattern(GetWorldPosCenter(i, j)));

            }
        }
        currentGrid = grids[0, 0];//默认选中第0个
        Debug.DrawLine(GetLeftUpWorldPos(0, num_h), GetLeftUpWorldPos(num_w, num_h), Color.black, 100f);
        Debug.DrawLine(GetLeftUpWorldPos(num_w, 0), GetLeftUpWorldPos(num_w, num_h), Color.black, 100f);
        //grids[5, 5].gridInst.SetActive(true);
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GetPosAndSetValue(GetCollidedPos(), 1);
        //    //Debug.Log("ChosenGrid1: " + grids.chosenGrid.pos[0] + grids.chosenGrid.pos[1]);
        //}
    }
    public Vector3 GetCollidedPos()
    {
        Vector3 posOnGround = Vector3.zero;
        Vector3 mp = Input.mousePosition;
        mp.z = 100;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mp);

        //Vector3 origin = my_camera.transform.position;
        //Vector3 origin = new Vector3(-5f, 5f, -5f);
        //Ray ray = new Ray(origin, pos);

        Ray ray = my_camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;                                                              //射线撞击点
        if (Physics.Raycast(ray, out hit))  //如果射线撞击到地面或者其他物体
        {
            posOnGround = hit.point;                                                 //新位置为射线撞击到地面的位置                        
        }

        //Debug.Log("pos:" + pos);
        //Debug.Log("ray:" + ray);
        //Debug.Log("Physics.Raycast(ray, out hit):" + Physics.Raycast(ray, out hit));
        //Debug.Log("posOnGround:" + posOnGround);
        return posOnGround;
    }


    //索引号是否有效
    bool IsValidIndex(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < num_w && z < num_h)
        { return true; }
        else return false;
    }
    bool IsValidIndex(Pos p)
    {
        return IsValidIndex(p.x, p.z);
    }
    List<Pos> FiltrateValidPos(List<Pos> l_pos)
    {
        List<Pos> nl_pos = new List<Pos>();
        foreach (Pos pos in l_pos)
        {
            if (IsValidIndex(pos))
            {
                nl_pos.Add(pos);
            }
        }
        //for(int i=0; i<l_pos.Count; )
        //{
        //    if (!IsValidIndex(l_pos[i]))
        //    {
        //        l_pos.RemoveAt(i);
        //    }
        //    else
        //    {
        //        i++;
        //    }
        //}
        return nl_pos;
    }

    float GetAltitude(int x, int z)
    {
        float altitude = 0;
        if (IsValidIndex(x, z)) {
            if(grids[x, z]) altitude = grids[x, z].altitude;
            else altitude = jsonParser.gridProperties.gridProperties[x * num_w + z].altitude;
        }
        return base_altitude + altitude * altitude_radio;
    }
    //偏移坐标(四角）
    public Vector3 GetLeftUpWorldPos(int x, int z)
    {
        float final_altitude = GetAltitude(x,z);
        return new Vector3(x, final_altitude, z) * cellSize + startPos;
    }
    public Vector3 GetLeftDownWorldPos(int x, int z)
    {
        float final_altitude = GetAltitude(x, z);
        return new Vector3(x, final_altitude, z+1) * cellSize + startPos;
    }
    public Vector3 GetRightUpWorldPos(int x, int z)
    {
        float final_altitude = GetAltitude(x, z);
        return new Vector3(x+1, final_altitude, z) * cellSize + startPos;
    }
    public Vector3 GetRightDownWorldPos(int x, int z)
    {
        float final_altitude = GetAltitude(x, z);
        return new Vector3(x+1, final_altitude, z+1) * cellSize + startPos;
    }
    //每个格子中心点的世界坐标
    public Vector3 GetWorldPosCenter(int x, int z)
    {
        float final_altitude = GetAltitude(x, z);
        return new Vector3(x+0.5f, final_altitude, z+0.5f) * cellSize + startPos;
    }
    //设置网格中的值
    public void SetValue(int x, int z, int value)
    {
        if (IsValidIndex(x, z))
        {
            arrayCell[x, z] = value;
            arrayTextMesh[x, z].text = value.ToString();

        }
    }
    public void SetChosenGrid(int x, int z)
    {
        if (IsValidIndex(x, z))
        {
            currentGrid = grids[x, z];
            //Debug.Log("ChosenGrid1: " + chosenGrid.pos[0] + chosenGrid.pos[1]);
        }
    }
    //将点击的坐标传入
    public void GetGridAndSetValue(int value=0)
    {
        int x, z;
        GetXZ(GetCollidedPos(), out x, out z);
        SetChosenGrid(x, z);
        SetValue(x, z, value);
 
    }

    //将点击的坐标转化为网格中位置
    public void GetXZ(Vector3 pos, out int x, out int z)
    {
        x = Mathf.FloorToInt((pos.x-startPos.x) / cellSize);

        z = Mathf.FloorToInt((pos.z-startPos.z) / cellSize);
    }

    //点击的表现，这里是修改网格内容
    public TextMesh SetText(string text, Vector3 pos, Color color, bool disable=true)
    {
        GameObject objText = new GameObject("WorldText", typeof(TextMesh))
        {
            layer = 5//UI
        };
        TextMesh textMesh = objText.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
        textMesh.alignment = TextAlignment.Center;
        textMesh.anchor = TextAnchor.MiddleLeft;
        textMesh.characterSize = 0.28f;
        objText.transform.position = pos;
        if (disable) objText.SetActive(false);//设置文字不可见
        return textMesh;
    }

    //设置格子图
    public GameObject SetGridPattern(GameObject _obj, Vector3 pos)
    {
        GameObject obj = Instantiate(_obj, pos+GridUnit.patternOffset, Quaternion.identity);
        return obj;
    }

    public bool PosCanMove(Pos pos, List<GridUnit> movePos)
    {
        if (movePos.Contains(GetGridByPos(pos))) { return true; }
        return false;
    }
    public void ShowMoveAndAttackGridsPattern(RoleUnit role)//, out List<Pos> movePos, out List<Pos> attackPos 后续把计算和显示分开
    {
        foreach (GridUnit grid in role.attackGrids)
        {
            grid.SetAttack();
        }
        foreach (GridUnit grid in role.moveGrids)
        {
            grid.SetChoose();
        }
    }
    public List<GridUnit> SetMoveableGridsBFS(RoleUnit role)
    {
        //所有节点walkPoint置INTMIN和father 置零
        ClearGridsWalkPointAndFather();

        Pos origin = role.GetPos();
        GridUnit originGrid = GetGridByPos(origin);
        originGrid.walkPoint = role.move_speed;
        print("SPEED" + role.move_speed);
        print("ROLEPOS" + origin.x + origin.z);

        List<GridUnit> moveableGrids = new List<GridUnit>();
        moveableGrids.Add(originGrid);

        Queue<GridUnit> q_grids = new Queue<GridUnit>();
        q_grids.Enqueue(originGrid);


        while(q_grids.Count > 0)
        {
            GridUnit fath = q_grids.Dequeue();
            List<GridUnit> sons = GetAdjaGrids(fath);
            foreach(GridUnit newgrid in sons)
            {
                //print("SONS" + newgrid.pos.x + newgrid.pos.z);
                //print("WALK" + (fath.walkPoint - newgrid.gridProperty.AccessibilityIndex));
                int accessCost = newgrid.CalcAccessCost(role);
                if (fath.walkPoint - accessCost >= 0)
                {
                    if (fath.walkPoint - accessCost > newgrid.walkPoint)   //剩余的点数多,即此路径更短
                    {
                        newgrid.walkPoint = fath.walkPoint - accessCost;
                        newgrid.father = fath;
                        q_grids.Enqueue(newgrid);
                    }
                    
                    if (!moveableGrids.Exists(_g => _g.pos==newgrid.pos) && !newgrid.roleOn)   //该格子上没有角色
                    {
                        //print("ADD" + newgrid.pos.x + newgrid.pos.z);
                        moveableGrids.Add(newgrid);
                    }
                }

            }
        }


        //foreach (GridUnit grid in moveableGrids)
        //{
        //    grid.SetActive(true);
        //}
        //foreach (Pos pos in moveablePos)
        //{
        //    GetGridByPos(pos).SetChoose();
        //}
        //print("GRIDSCOUNT"+moveableGrids.Count);
        return moveableGrids;
    }

    //设置能攻击到的格子
    public List<GridUnit> SetAttackableGrids(RoleUnit role)
    {
        List<GridUnit> attackableGrids = new List<GridUnit>();

        List<GridUnit> move = role.moveGrids;
        int minRange = role.attack_dist;
        int maxRange = role.attack_dist;

        foreach (GridUnit grid in move)
        {
            for(int i=minRange; i<=maxRange; i++)
            {
                List<GridUnit> gs = GetGridByPos(FiltrateValidPos(grid.pos.GetEquidistanceGrids(i)));
                foreach(GridUnit g in gs)
                {
                    if (!attackableGrids.Exists(_g => _g.pos == g.pos)) { attackableGrids.Add(g); } //注意，这个条件才能排除重复的pos
                }
            }
        }

        //print("MOVE" + move.Count + "ATTACK" + attackableGrids.Count);
        //foreach (Pos pos in attackablePos)
        //{
        //    //如果不属于可移动范围 则格子变为红色
        //    //if (!move.Contains(pos))
        //    //{
        //    //    GetGridByPos(pos).SetAttack();
        //    //}
        //    if (!GetGridByPos(pos).patternChoose.active)
        //    {
        //        GetGridByPos(pos).SetAttack();
        //    }
        //}
        return attackableGrids;
    }

    public void RoleFindPathAndMove(RoleUnit atkRole, RoleUnit defRole)
    {
        //print("PATHFINDING");
        List<GridUnit> path = AStarPathFinding(atkRole, defRole);
        RoleMoveOnPath(atkRole, path);
    }
    public List<GridUnit> AStarPathFinding(RoleUnit atkRole, RoleUnit defRole)
    {
        //print("ASTAR_START");
        ClearGridsWalkPointAndFather();

        List<GridUnit> move = atkRole.moveGrids;    //可移动的节点
        List<GridUnit> close = new List<GridUnit>();  //已求出的最优节点
        List<GridUnit> open = new List<GridUnit>();   //备选的最佳节点

        Pos origin = atkRole.pos;
        Pos dest = defRole.pos;

        GridUnit oriGrid = GetGridByPos(origin);
        oriGrid.GValue = 0;
        oriGrid.HValue = Pos.CalcMagnitude(origin, dest);
        oriGrid.FValue = GetGridByPos(origin).HValue;

        GridUnit destGrid = GetGridByPos(dest);

        open.Add(oriGrid);

        while (open.Count > 0)
        {
            //print("OPEN_COUNT" + open.Count);
            if (GridsContains(open, destGrid))
            {
                //print("BINGO");
                break;
            }

            //找到F值最小的
            open.Sort((x, y) =>
            {
                return x.FValue.CompareTo(y.FValue);
            });
            GridUnit fath = open[0];
            open.RemoveAt(0);
            if (!GridsContains(close, fath))
            {
                close.Add(fath);
            }

            //找F值最小节点的邻接节点
            List<GridUnit> adjaGrids = GetGridByPos(FiltrateValidPos(fath.pos.GetEquidistanceGrids(1)));
            foreach(GridUnit grid in adjaGrids)
            {
                if (!GridsContains(close, grid))
                {
                    float Gtemp = fath.GValue + grid.CalcAccessCost(atkRole); ;
                    float Htemp = Pos.CalcMagnitude(grid.pos, dest);
                    float Ftemp = Gtemp + Htemp;

                    //如果不在open列表里或者值更小就更新值和父亲
                    if(!GridsContains(open, grid) || Ftemp < grid.FValue)
                    {
                        grid.FValue = Ftemp;
                        grid.GValue = Gtemp;
                        grid.HValue = Htemp;
                        grid.father = fath;
                        if (!GridsContains(open, grid))
                        {
                            open.Add(grid);
                        }
                    }
                }
            }

        }
        List<GridUnit> path = new List<GridUnit>();
        GridUnit g = destGrid;
        while(true)
        {
            //print("GXZ" + g.pos.x + g.pos.z);
            //显示路线

            if (g.pos == oriGrid.pos) break;
            if (GridsContains(move, g))
            {
                path.Add(g);
                //g.SetAttack();
            }
            g = g.father;
        }
        path.Reverse();
        return path;

    }

    public void RoleMoveOnPath(RoleUnit role, List<GridUnit> path)
    {
        foreach (GridUnit grid in path)
        {
            role.SetPos(grid.pos);
        }
    }
    //IEnumerator RoleMoveOnPath(RoleUnit role, List<GridUnit> path)
    //{
    //    foreach (GridUnit grid in path)
    //    {
    //        role.SetPos(grid.pos);
    //    }
    //    yield return new WaitForSeconds(0.2f);
    //}

    //void PathFindingDijkstra(GridUnit originGrid)
    //{
    //    Pos origin = originGrid.pos;
    //    int[,] dist = new int[num_w, num_h];
    //    bool[,] vis = new bool[num_w, num_h];
    //    //System.Array.ForEach(dist, n => n = new int[]());
    //    for(int i=0; i<num_h; i++)
    //    {
    //        for(int j=0; j<num_w; j++)
    //        {
    //            if (i == origin.x && j == origin.z) { dist[i, j] = 0; } //初始格
    //            else if (i == origin.x - 1 && j == origin.z ||
    //                    i == origin.x + 1 && j == origin.z ||
    //                    i == origin.x && j == origin.z - 1 ||
    //                    i == origin.x && j == origin.z + 1)
    //                 { dist[i, j] = grids[i, j].gridProperty.AccessibilityIndex; } //邻接格
    //            else { dist[i, j] = int.MaxValue; }//其他格
    //            vis[i, j] = false;
    //        }
    //    }

    //    dist[origin.x, origin.z] = 0;
    //    while (true)
    //    {
    //        for (int i = 0; i < num_h; i++)
    //        {
    //            for (int j = 0; j < num_w; j++)
    //            {
    //                dist[i, j] = int.MaxValue;
    //                vis[i, j] = false;
    //            }
    //        }
    //    }


    //}

    public GridUnit GetGridByPos(Pos p)
    {
        return grids[p.x, p.z];
    }
    public List<GridUnit> GetGridByPos(List<Pos> ps)
    {
        List<GridUnit> l_grids = new List<GridUnit>();
        foreach(Pos p in ps)
        {
            l_grids.Add(GetGridByPos(p));
        }
        return l_grids;
    }

    List<GridUnit> GetAdjaGrids(GridUnit grid)
    {
        List<GridUnit> res = new List<GridUnit>();
        Pos pos= grid.pos;
        if (IsValidIndex(pos.GetLeft())) res.Add(GetGridByPos(pos.GetLeft()));
        if (IsValidIndex(pos.GetRight())) res.Add(GetGridByPos(pos.GetRight()));
        if (IsValidIndex(pos.GetUp())) res.Add(GetGridByPos(pos.GetUp()));
        if (IsValidIndex(pos.GetDown())) res.Add(GetGridByPos(pos.GetDown()));

        return res;
    }

    void ClearGridsWalkPointAndFather()
    {
        foreach(GridUnit grid in grids)
        {
            grid.walkPoint = int.MinValue;
            grid.father = grid;
        }
    }
    public void ClearGridsPattern()
    {
        foreach(GridUnit grid in grids)
        {
            grid.SetReset();
        }
    }

    public bool GridsContains(List<GridUnit>l_grids, GridUnit g)
    {
        return l_grids.Exists(_g => _g.pos == g.pos);
    }
}

