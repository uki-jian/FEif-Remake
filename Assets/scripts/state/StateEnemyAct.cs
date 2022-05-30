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
        manager.currentStateType = BattleStateType.enemy_choose_unit; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        //manager.TransitionState(BattleStateType.choose_unit); //ս����ʼ�󣬽����ҷ�ѡ�˽׶�
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateBattleEnemyAct");
    }
}
