using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBattleStart : IState
{
    public FSMBattleState manager;
    
    public StateBattleStart(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateBattleStart");
        manager.currentStateType = BattleState.battle_start; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬
        
        
    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        manager.TransitionState(BattleState.choose_unit); //ս����ʼ�󣬽����ҷ�ѡ�˽׶�
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateBattleStart");
    }
}
