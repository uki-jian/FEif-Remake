using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBattleEndLose : MonoBehaviour, IState
{
    public FSMBattleState manager;

    public StateBattleEndLose(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateBattleEndLose");
        manager.currentStateType = BattleState.battle_end_lose; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        //manager.TransitionState(BattleStateType.choose_unit); //ս����ʼ�󣬽����ҷ�ѡ�˽׶�
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateBattleEndLose");
    }
}
