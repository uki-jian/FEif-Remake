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
    public void OnEnter() //进入这个状态应该进行的方法
    {
        Debug.Log("STATE_START: StateBattleStart");
        manager.currentStateType = BattleState.battle_start; //让控制器脚本的当前状态改成该状态
        
        
    }
    public void OnUpdate() //维持这个状态的方法
    {
        manager.TransitionState(BattleState.choose_unit); //战斗开始后，进入我方选人阶段
    }
    public void OnExit() //退出这个状态应该执行的方法
    {
        Debug.Log("STATE_END: StateBattleStart");
    }
}
