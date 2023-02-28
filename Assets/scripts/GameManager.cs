using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridManager gridManager;
    public RoleManager unitManager;
    public JsonManager jsonManager;
    public BattleManager battleManager;
    
    //int num_w, num_h;


    //移动某个单位到某个格点上
    //public bool MoveUnit2Grid(UnitClass unit, int x, int z)
    //{
    //    if (x < 0 || x > num_w) return false;
    //    if (z < 0 || z > num_h) return false;
    //    if (x != unit.pos[0] || z != unit.pos[1])
    //    {
    //        unit.pos[0] = x;
    //        unit.pos[1] = z;

    //        return true;
    //    }
    //    return false;
    //}
    void Update()
    {
        //gridManager.grids[5, 5].pattern.SetActive(true);
        UpdateChosenGrid();
        UpdateChosenUnit();
        UpdateUnitMap();
        
        UpdateRoleMoveablePos();
        UpdateUnitOnGrid();
        UpdateUnitImagePos();
        //gridManager.AStarPathFinding(unitManager.units[0], unitManager.units[1]);
    }
    void UpdateUnitOnGrid()
    {
        foreach(GridUnit grid in gridManager.grids)
        {
            grid.roleOn = null;
        }
        foreach(RoleUnit role in unitManager.units)
        {
            if (!role.isDead) gridManager.GetGridByPos(role.pos).roleOn = role;
        }
    }

    void UpdateRoleMoveablePos()
    {
        //print("UNITS_NUM" + unitManager.units.Length);
        foreach (RoleUnit unit in unitManager.units)
        {
            unit.moveGrids = gridManager.SetMoveableGridsBFS(unit);
            unit.attackGrids = gridManager.SetAttackableGrids(unit);
        }
    }


    //重构
    void UpdateUnitImagePos()
    {
        foreach(RoleUnit unit in unitManager.units)
        {
            int x = unit.pos.x, z = unit.pos.z;
            Vector3 pos = gridManager.GetWorldPosCenter(x, z);
            unit.avater.transform.position = pos;
            if (unit.HasCloneAvatar())
            {
                int dx = unit.displayedPos.x, dz = unit.displayedPos.z;
                Vector3 displayedPos = gridManager.GetWorldPosCenter(dx, dz);

                if (unit.DisplayedPosEquals2Pos()) { unit.displayedAvatar.SetActive(false); }
                else { unit.displayedAvatar.SetActive(true); }

                //if(gridManager.PosCanMove(unit.displayedPos, gridManager.SetMoveableGridsBFS(unit)))unit.displayedAvatar.transform.position = displayedPos;//修改
                if (gridManager.GridsContains(unit.moveGrids, gridManager.GetGridByPos(unit.displayedPos)))
                {
                    unit.displayedAvatar.transform.position = displayedPos;
                }
            }
        }
    }
    public GridUnit GetChosenGrid()
    {
        return gridManager.currentGrid;
    }
    public RoleUnit GetChosenUnit()
    {
        //return unitManager.chosenUnit;
        return GetChosenGrid().roleOn;
    }
    //选中角色是否能到达某处？如果能到达则执行
    public bool UnitReachGrid(RoleUnit unit, GridUnit grid)
    {
        return UnitReachGrid(unit, grid.pos);
    }
    public bool UnitReachGrid(RoleUnit unit, Pos gridPos)
    {
        return unit.SetPos(gridPos);
    }
    //displayed move
    public void CloneUnitAvatar(RoleUnit unit)
    {
        unit.CloneAvatar();
    }
    public void DistroyCloneUnitAvatar(RoleUnit unit)
    {
        unit.DistroyCloneAvatar();
    }
    public bool UnitDisplayedMove(RoleUnit unit, GridUnit grid)
    {
        //unit.CloneAvatar();
        bool can_move = UnitDisplayedMove(unit, grid.pos);
        //unit.DistroyCloneAvatar();
        return can_move;
    }
    public bool UnitDisplayedMove(RoleUnit unit, Pos gridPos)
    {
        return unit.SetDisplayedPos(gridPos);
    }
    public void UnitMove2ClonePos(RoleUnit unit)
    {
        unit.Move2Clone();
    }
    void UpdateChosenGrid()
    {
        gridManager.GetGridAndSetValue();
    }
    //每帧更新被选择的角色
    void UpdateChosenUnit()  
    {
        bool unitIsChosen = false;
        foreach (RoleUnit unit in unitManager.units)
        {
            //Debug.Log("unit: " + unit.pos.x + unit.pos.z);
            //Debug.Log("grid: " + gridManager.currentGrid.pos.x + gridManager.currentGrid.pos.z+"MOVE"+ gridManager.currentGrid.gridProperty.AccessibilityIndex);
            //RefreshUnitImagePos(unit);
            if (!unit.IsMoved && Pos.Equal(unit.pos, gridManager.currentGrid.pos))
            {
                unitManager.chosenUnit = unit;
                unitIsChosen = true;
                //Debug.Log("ChosenUnit: " + unitManager.chosenUnit.pos.x + unitManager.chosenUnit.pos.z);
                //UnitMoveStat(unit);
                //print("CHOOSE");
                

            }
        }

        //Debug.Log("unitIsChosen: " + unitIsChosen);
        if (!unitIsChosen) unitManager.chosenUnit = null;

    }


    //每帧更新unitmap
    void UpdateUnitMap()
    {
        for (int i = 0; i < gridManager.num_h; i++)
        {
            for (int j = 0; j < gridManager.num_w; j++)
            {
                gridManager.unitMap[i, j] = -1;
                //Debug.Log("gridManager.isUnitOnGrid:" + gridManager.isUnitOnGrid[i, j]);
            }
        }
        foreach (RoleUnit unit in unitManager.units)
        {
            gridManager.unitMap[unit.pos.z, unit.pos.x] = unit.unitID;
            //Debug.Log("isUnitOnGrid" + unit.pos[1] + unit.pos[0]);
        }
        //Debug.Log("isUnitOnGrid" + unitManager.units.Length);
    }
    
    
    
    public int GetUnitArmy()
    {
        if (GetChosenUnit() == null) return -1;
        else return GetChosenUnit().unitProperty.u_army;
    }
    public bool IsChosenUnitMyArmy()
    {
        return GetUnitArmy() == 0;
    }
    public void UnitsBattle(RoleUnit own, RoleUnit foe)
    {
        if(own && foe)
            battleManager.Battle(own, foe);
        //own.IsMoved = true;//攻击方本回合已行动
    }
    public int CountEnemyLived()
    {
        int cnt = 0;
        foreach(RoleUnit unit in unitManager.units)
        {
            if (!unit.isDead && unit.unitProperty.u_army != unitManager.myArmyIndex) cnt++;
        }
        return cnt;
    }
    public int CountMyArmyLived()
    {
        int cnt = 0;
        foreach (RoleUnit unit in unitManager.units)
        {
            if (!unit.isDead && unit.unitProperty.u_army == unitManager.myArmyIndex) cnt++;
        }
        return cnt;
    }
    //void UnitMoveStat(UnitClass unit)
    //{
    //    if (Input.GetMouseButtonDown(1))    //如果按下，就转到位置
    //    {
    //        //gridManager.GetPosAndSetValue(gridManager.GetCollidedPos(), 2); //得到选择的格子
    //        //Debug.Log("MOVED");
    //        if (unit.SetPos(gridManager.chosenGrid.pos))//设置位置
    //        {
    //            unit.chooseAble = false;    //该回合行动结束
    //        }
    //            unitManager.chosenUnit = null;
    //    }


    //    if (!unit.isDead && !unitManager.units[1].isDead)
    //        UnitsBattle.Battle(unitManager.units[0], unitManager.units[1]);
    //}
}
