using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager: RoleManager
{
    //一次战斗
    static public void Battle(RoleUnit own, RoleUnit foe)
    {
        Debug.Log("BATTLED!!!");
        int dist = Calc2UnitsDistance(own.pos, foe.pos);
        if(dist <= own.attack_dist)
        {
            foe.remained_hitpoints -= own.attack_points;    //我方攻击
        }
        
        if(dist <= foe.attack_dist && foe.remained_hitpoints > 0)
        {
            own.remained_hitpoints -= foe.attack_points;    //敌方攻击
        }
        
    }
}
