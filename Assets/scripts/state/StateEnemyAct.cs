using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyAct : IState
{
    public FSMBattleState manager;

    public StateEnemyAct(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateBattleEnemyAct");
        manager.currentStateType = BattleState.enemy_act; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        //manager.TransitionState(BattleStateType.choose_unit); //ս����ʼ�󣬽����ҷ�ѡ�˽׶�
        if (FSM.GetConfirmation())
        {
            manager.EnemyAttack();
            manager.TransitionState(BattleState.choose_unit);
        }

    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateBattleEnemyAct");
        manager.factory.unitManager.ResetRoleMoveFlag();
    }
}
