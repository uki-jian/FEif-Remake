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
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateChooseAttackObj");
        manager.currentStateType = BattleStateType.choose_attack_obj; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        if (FSM.GetConfirmation())
        {
            //ѡ��з�����
            if (!manager.factory.IsChosenUnitMyArmy())
            {
                manager.SetDefenceUnit();
                manager.UnitsBattle();
                
            }
            //ѡ�������ط������
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
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateChooseAttackObj");
    }
}
