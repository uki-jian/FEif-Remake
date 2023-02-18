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
    public void OnEnter() //进入这个状态应该进行的方法
    {
        Debug.Log("STATE_START: StateChosenMovePos");
        manager.currentStateType = BattleStateType.choose_move_pos; //让控制器脚本的当前状态改成该状态
        
        

    }
    public void OnUpdate() //维持这个状态的方法
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
    public void OnExit() //退出这个状态应该执行的方法
    {
        Debug.Log("STATE_END: StateChosenMovePos");
        manager.factory.gridManager.ClearGridsPattern();//结束显示可移动范围
    }
}
