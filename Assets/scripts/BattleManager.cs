using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager: RoleManager
{
    //һ��ս��
    static public void Battle(RoleUnit own, RoleUnit foe)
    {
        Debug.Log("BATTLED!!!");
        int dist = Calc2UnitsDistance(own.pos, foe.pos);
        if(dist <= own.attack_dist)
        {
            foe.remained_hitpoints -= own.attack_points;    //�ҷ�����
        }
        
        if(dist <= foe.attack_dist && foe.remained_hitpoints > 0)
        {
            own.remained_hitpoints -= foe.attack_points;    //�з�����
        }
        
    }
}
