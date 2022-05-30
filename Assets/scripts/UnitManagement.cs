using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManagement : MonoBehaviour
{
    public UnitClass[] units;
    public UnitClass chosenUnit;
    public JsonParser jsonParser;

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
        foreach (UnitClass unit in units)
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
    public bool IsUnitMyArmy(UnitClass unit)
    {
        return unit.unitProperty.u_army == myArmyIndex;
    }
    
    static public int Calc2UnitsDistance(Pos src, Pos dst)
    {
        //System.Diagnostics.Debug.Assert(src.Length == 2);
        //System.Diagnostics.Debug.Assert(dst.Length == 2);
        return System.Math.Abs(dst.x - src.x) + System.Math.Abs(dst.z - src.z);
        
    }
    
}
