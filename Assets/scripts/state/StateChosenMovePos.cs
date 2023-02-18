using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChosenMovePos : IState
{
    public FSMBattleState manager;

    public StateChosenMovePos(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateChosenMovePos");
        manager.currentStateType = BattleStateType.choose_move_pos; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬
        
        

    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        
        manager.CloneAttackUnit();
        if (manager.AttackUnitTryReach())
        {
            if (FSM.GetConfirmation()){
                manager.MoveAttackUnitPos2CloneAndDistroyClone();
                manager.TransitionState(BattleStateType.choose_attack_obj);
            }
            
        }
        if (FSM.GetCancell())
        {
            manager.DistroyCloneAttackUnit();
            manager.TransitionState(BattleStateType.choose_unit);
        }
        
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateChosenMovePos");
        manager.factory.gridManager.ClearGridsPattern();//������ʾ���ƶ���Χ
    }
}
