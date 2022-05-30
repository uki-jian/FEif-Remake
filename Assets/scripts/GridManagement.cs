using UnityEngine;

public class GridManagement : MonoBehaviour
{
    public MeshRenderer meshFilter;
    public Camera my_camera;
    public JsonParser jsonParser;

    public int[,] unitMap;//�Ƿ�����վ��ĳ��������,ÿ������������ı�ţ�������Ϊ-1��

    Vector3 scale;      //����ߴ�
    public int num_w; //���ȸ���
    public int num_h; //�߶ȸ���
    public float base_altitude = 0.5f;  //y���׼�߶�
    public float altitude_radio = 1f; //y������ϵ��
    float[] delta_pos;
    float cellSize;
    public Vector3 startPos;  //�������
    public int[,] arrayCell;  //�������飬�洢��������
    public TextMesh[,] arrayTextMesh;      //�洢����textmesh����������������
    public GridClass[,] grids;    //ÿ�����������
    public GridClass chosenGrid;   //����ѡ�еĸ���

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

        //Debug.Log("width��" + width + " height: " + height);
        //Debug.Log("num_w��" + num_w + " num_h: " + num_h);
        //Debug.Log("cellSize��" + cellSize);
        //Debug.Log("delta_pos��" + delta_pos[0]);
        
        arrayCell = new int[num_w, num_h];
        arrayTextMesh = new TextMesh[num_w, num_h];


        unitMap = new int[num_w, num_h];
        for(int i=0; i<num_h; i++)
        {
            for(int j=0; j<num_w; j++)
            {
                unitMap[i, j] = -1;
            }
        }
        grids = new GridClass[num_w, num_h];   //����ÿ�����������
        

        //Debug.Log("grid: " + myGrid[0,0].pos_i);
        //Debug.Log("grid: " + myGrid[0, 0].pos_i + myGrid[0, 0].pos_j);

        for (int i = 0; i < num_w; i++)
        {
            for (int j = 0; j < num_h; j++)
            {
                grids[i, j] = new GridClass(jsonParser.gridProperties.gridProperties[i*num_w + j]);
                grids[i, j].SetPos(i, j);
                arrayTextMesh[i, j] = SetText(arrayCell[i, j].ToString(), GetWorldPosCenter(i, j), Color.red);
                Debug.DrawLine(GetLeftUpWorldPos(i, j), GetRightUpWorldPos(i, j), Color.black, 100f);
                Debug.DrawLine(GetLeftUpWorldPos(i, j), GetLeftDownWorldPos(i, j), Color.black, 100f);
            }
        }
        chosenGrid = grids[0,0];//Ĭ��ѡ�е�0��
        Debug.DrawLine(GetLeftUpWorldPos(0, num_h), GetLeftUpWorldPos(num_w, num_h), Color.black, 100f);
        Debug.DrawLine(GetLeftUpWorldPos(num_w, 0), GetLeftUpWorldPos(num_w, num_h), Color.black, 100f);

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

        RaycastHit hit;                                                              //����ײ����
        if (Physics.Raycast(ray, out hit))  //�������ײ�������������������
        {
            posOnGround = hit.point;                                                 //��λ��Ϊ����ײ���������λ��                        
        }

        //Debug.Log("pos:" + pos);
        //Debug.Log("ray:" + ray);
        //Debug.Log("Physics.Raycast(ray, out hit):" + Physics.Raycast(ray, out hit));
        //Debug.Log("posOnGround:" + posOnGround);
        return posOnGround;
    }


    //�������Ƿ���Ч
    bool IsValidIndex(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < num_w && z < num_h)
        { return true; }
        else return false;
    }
    float GetAltitude(int x, int z)
    {
        float altitude = 0;
        if (IsValidIndex(x, z)) { altitude = grids[x, z].altitude; }
        return base_altitude + altitude * altitude_radio;
    }
    //ƫ������(�Ľǣ�
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
    //ÿ���������ĵ����������
    public Vector3 GetWorldPosCenter(int x, int z)
    {
        float final_altitude = GetAltitude(x, z);
        return new Vector3(x+0.5f, final_altitude, z+0.5f) * cellSize + startPos;
    }
    //���������е�ֵ
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
            chosenGrid = grids[x, z];
            //Debug.Log("ChosenGrid1: " + chosenGrid.pos[0] + chosenGrid.pos[1]);
        }
    }
    //����������괫��
    public void GetGridAndSetValue(int value=0)
    {
        int x, z;
        GetXZ(GetCollidedPos(), out x, out z);
        SetChosenGrid(x, z);
        SetValue(x, z, value);
 
    }

    //�����������ת��Ϊ������λ��
    public void GetXZ(Vector3 pos, out int x, out int z)
    {
        x = Mathf.FloorToInt((pos.x-startPos.x) / cellSize);

        z = Mathf.FloorToInt((pos.z-startPos.z) / cellSize);
    }

    //����ı��֣��������޸���������
    public TextMesh SetText(string text, Vector3 pos, Color color)
    {
        GameObject objText = new GameObject("WorldText", typeof(TextMesh));
        TextMesh textMesh = objText.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
        textMesh.alignment = TextAlignment.Center;
        textMesh.anchor = TextAnchor.MiddleLeft;
        textMesh.characterSize = 0.28f;
        objText.transform.position = pos;
        return textMesh;
    }

}

