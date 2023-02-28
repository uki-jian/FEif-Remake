using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChooseUnit : MonoBehaviour, IState
{
    public FSMBattleState manager;
    
    public StateChooseUnit(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateChooseUnit");
        manager.currentStateType = BattleState.choose_unit; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬


    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        
        if (FSM.GetConfirmation() && manager.factory.unitManager.chosenUnit)
        {
            //���ѡ�е������ҷ���ɫ
            if (manager.factory.IsChosenUnitMyArmy())
            {
                manager.SetAttackUnit();
                manager.TransitionState(BattleState.choose_move_pos);
            }
            //���ѡ�е����ǵз���ɫ
            else
            {
                manager.factory.unitManager.chosenUnit.ShowDangerZone();
            }
        }
        if (FSM.GetCancell())
        {
            manager.ResetAttackUnit();
        }
        
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateChooseUnit");
        manager.factory.gridManager.ShowMoveAndAttackGridsPattern(manager.factory.unitManager.chosenUnit);//��ʾ���ƶ���Χ\��ʾ������Χ
    }
}
