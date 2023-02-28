using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager: RoleManager
{
    public GameManager gameManager;
    //一次战斗
    public void Battle(RoleUnit own, RoleUnit foe)
    {
        Debug.Log("BATTLED!!!"+"own"+ own.attack_points + "foe"+ foe.attack_points);
        int dist = Calc2UnitsDistance(own.pos, foe.pos);
        //print("DIST" + dist);
        if(dist <= own.attack_dist)
        {
            foe.remained_hitpoints -= own.attack_points;    //我方攻击
        }
        
        if(dist <= foe.attack_dist && foe.remained_hitpoints > 0)
        {
            own.remained_hitpoints -= foe.attack_points;    //敌方攻击
        }
        own.tryDie();
        foe.tryDie();
        if (IsBattleLose())
        {
            print("You Lose!!!");
        }
        else if (IsBattleWin())
        {
            print("You Win!!!");
        }
    }
    public bool IsBattleWin()
    {
        //敌人数为零则获胜
        if (gameManager.CountEnemyLived() > 0)
        {
            return false;
        }
        else return true;
    }
    public bool IsBattleLose()
    {
        //己方人数为零则失败
        if (gameManager.CountMyArmyLived() > 0)
        {
            return false;
        }
        else return true;
    }
}
