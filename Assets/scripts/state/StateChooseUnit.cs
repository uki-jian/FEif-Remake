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
    public void OnEnter() //进入这个状态应该进行的方法
    {
        Debug.Log("STATE_START: StateChooseUnit");
        manager.currentStateType = BattleState.choose_unit; //让控制器脚本的当前状态改成该状态


    }
    public void OnUpdate() //维持这个状态的方法
    {
        
        if (FSM.GetConfirmation() && manager.factory.unitManager.chosenUnit)
        {
            //如果选中的人是我方角色
            if (manager.factory.IsChosenUnitMyArmy())
            {
                manager.SetAttackUnit();
                manager.TransitionState(BattleState.choose_move_pos);
            }
            //如果选中的人是敌方角色
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
    public void OnExit() //退出这个状态应该执行的方法
    {
        Debug.Log("STATE_END: StateChooseUnit");
        manager.factory.gridManager.ShowMoveAndAttackGridsPattern(manager.factory.unitManager.chosenUnit);//显示可移动范围\显示攻击范围
    }
}
