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
    public void OnEnter() //进入这个状态应该进行的方法
    {
        Debug.Log("STATE_START: StateChooseUnit");
        manager.currentStateType = BattleStateType.choose_unit; //让控制器脚本的当前状态改成该状态


    }
    public void OnUpdate() //维持这个状态的方法
    {
        //如果选中的人是我方角色
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
    public void OnExit() //退出这个状态应该执行的方法
    {
        Debug.Log("STATE_END: StateChooseUnit");
    }
}
