using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChooseUnit : IState
{
    public FSMBattleState manager;
    
    public StateChooseUnit(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateChooseUnit");
        manager.currentStateType = BattleStateType.choose_unit; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        //���ѡ�е������ҷ���ɫ
        if (FSM.GetConfirmation() && manager.factory.IsChosenUnitMyArmy())
        {
            manager.SetAttackUnit();
            
            manager.TransitionState(BattleStateType.choose_move_pos);
        }
        if (FSM.GetCancell())
        {
            manager.ResetAttackUnit();
        }
        
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateChooseUnit");
        manager.factory.gridManager.SetMoveAndAttackGridsPattern(manager.factory.unitManager.chosenUnit);//��ʾ���ƶ���Χ\��ʾ������Χ
    }
}
