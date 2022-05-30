using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFactory: UnitManagement
{
    //һ��ս��
    static public void Battle(UnitClass own, UnitClass foe)
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
