using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChooseAttackObj : MonoBehaviour, IState
{
    public FSMBattleState manager;

    public StateChooseAttackObj(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //进入这个状态应该进行的方法
    {
        Debug.Log("STATE_START: StateChooseAttackObj");
        manager.currentStateType = BattleState.choose_attack_obj; //让控制器脚本的当前状态改成该状态
        //manager.factory.gridManager.ShowAttackGridsPattern(manager.chosenUnit);
        manager.factory.gridManager.SetAndChooseAttackGrid(manager.chosenUnit);

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


            //if (manager.IsBattleLose())
            //{
            //    manager.TransitionState(BattleState.battle_end_lose);
            //}
            //else if (manager.IsBattleWin())
            //{
            //    manager.TransitionState(BattleState.battle_end_win);
            //}
            if (manager.IsAllMyArmyMoved())
            {
                print("MOVEDDDDDD");
                manager.TransitionState(BattleState.enemy_act);
            }
            else
            {
                manager.TransitionState(BattleState.choose_unit);
            }
        }

        if (FSM.GetCancell())
        {
            manager.ResetDefenceUnit();
            manager.TransitionState(BattleState.choose_move_pos);
        }
        
    }
    public void OnExit() //退出这个状态应该执行的方法
    {
        Debug.Log("STATE_END: StateChooseAttackObj");
        //manager.EnemyAttack_();
        manager.factory.gridManager.ClearGridsPattern();
    }
}
