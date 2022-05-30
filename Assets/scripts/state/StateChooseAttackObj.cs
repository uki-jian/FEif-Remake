using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChooseAttackObj : IState
{
    public FSMBattleState manager;

    public StateChooseAttackObj(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //进入这个状态应该进行的方法
    {
        Debug.Log("STATE_START: StateChooseAttackObj");
        manager.currentStateType = BattleStateType.choose_attack_obj; //让控制器脚本的当前状态改成该状态


    }
    public void OnUpdate() //维持这个状态的方法
    {
        if (FSM.GetConfirmation())
        {
            //选择敌方攻击
            if (!manager.factory.IsChosenUnitMyArmy())
            {
                manager.SetDefenceUnit();
                manager.UnitsBattle();
                
            }
            //选择其他地方则待机
            if (manager.IsChosenUnit())
            {
                //manager.TransitionState(BattleStateType.battle_start);
            }
            manager.AttackUnitMoved();


            if (manager.IsBattleLose())
            {
                manager.TransitionState(BattleStateType.battle_end_lose);
            }
            if (manager.IsBattleWin())
            {
                manager.TransitionState(BattleStateType.battle_end_win);
            }
            if (manager.IsAllMyArmyMoved())
            {
                manager.TransitionState(BattleStateType.enemy_choose_unit);
            }
            manager.TransitionState(BattleStateType.choose_unit);
        }

        if (FSM.GetCancell())
        {
            manager.ResetDefenceUnit();
            manager.TransitionState(BattleStateType.choose_move_pos);
        }
        
    }
    public void OnExit() //退出这个状态应该执行的方法
    {
        Debug.Log("STATE_END: StateChooseAttackObj");
    }
}
