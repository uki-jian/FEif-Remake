using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBattleEndWin : IState
{
    public FSMBattleState manager;

    public StateBattleEndWin(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateBattleEndWin");
        manager.currentStateType = BattleState.battle_end_win; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        //manager.TransitionState(BattleStateType.choose_unit); //ս����ʼ�󣬽����ҷ�ѡ�˽׶�
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateBattleEndWin");
    }
}
