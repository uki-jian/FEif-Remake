using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{
    public RoleUnit[] units;
    List<RoleUnit> units_myarmy;
    List<RoleUnit> units_enemy;
    List<RoleUnit> units_ally;

    public RoleUnit chosenUnit;
    public JsonManager jsonParser;

    public int myArmyIndex;
    private void Start()
    {
        myArmyIndex = 0;
    }
    private void Update()
    {
        
            
    }
    public int CountArmyUnitUnmoved(int unitArmy)
    {
        bool allUnitsActed = true;
        int cnt = 0;
        foreach (RoleUnit unit in units)
        {
            if (unit.unitProperty.u_army == unitArmy && !unit.IsMoved)
            {
                cnt++;
                allUnitsActed = false;
            }
        }
        return cnt;
        //if (allUnitsActed)  //若所有角色都行动过，则开启下一回合（所有角色又可以再次行动）
        //{
        //    foreach (UnitClass unit in units)
        //    {
        //        unit.isMoved = true;
        //    }
        //}

    }
    public bool IsUnitMyArmy(RoleUnit unit)
    {
        return unit.unitProperty.u_army == myArmyIndex;
    }
    
    static public int Calc2UnitsDistance(Pos src, Pos dst)
    {
        //System.Diagnostics.Debug.Assert(src.Length == 2);
        //System.Diagnostics.Debug.Assert(dst.Length == 2);
        return System.Math.Abs(dst.x - src.x) + System.Math.Abs(dst.z - src.z);
        
    }
    public int Calc2UnitsDistance(RoleUnit src, RoleUnit dst)
    {
        //System.Diagnostics.Debug.Assert(src.Length == 2);
        //System.Diagnostics.Debug.Assert(dst.Length == 2);
        return System.Math.Abs(dst.pos.x - src.pos.x) + System.Math.Abs(dst.pos.z - src.pos.z);

    }

    public RoleUnit EnemyFindAttackObject(RoleUnit atk_role)
    {
        RoleUnit obj_role = units[0];
        int min_dist = int.MaxValue;
        foreach(RoleUnit role in units)
        {
            if (IsUnitMyArmy(role))
            {
                int dist = Calc2UnitsDistance(atk_role, role);
                if(dist < min_dist)
                {
                    min_dist = dist;
                    obj_role = role;
                }
            }
        }
        return obj_role;
    }
    public void ResetRoleMoveFlag()
    {
        foreach(RoleUnit role in units)
        {
            role.IsMoved = false;
        }
    }

}
