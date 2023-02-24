using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBattleState : FSM
{
    public IState currentState;//当前状态
    public BattleState currentStateType;//当前状态类型
    public Dictionary<BattleState, IState> states = new Dictionary<BattleState, IState>();//状态字典
    public GameManager factory;

    public RoleUnit attackUnit;//进攻单位
    public RoleUnit defenceUnit;//防守单位

    public RoleUnit chosenUnit;
    public GridUnit chosenGrid;

    public StateBattleStart stateBattleStart;
    public StateChooseUnit stateChooseUnit;
    public StateChosenMovePos stateChosenMovePos;
    public StateChooseAttackObj stateChooseAttackObj;
    public StateEnemyAct stateEnemyAct;
    public StateBattleEndWin stateBattleEndWin;
    public StateBattleEndLose stateBattleEndLose;

    Pos lastPos;
    int myArmyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        #region
        states.Add(BattleState.battle_start, stateBattleStart);
        states.Add(BattleState.choose_unit, stateChooseUnit);
        states.Add(BattleState.choose_move_pos, stateChosenMovePos);
        //states.Add(BattleState.choose_attack_obj, new StateChooseAttackObj(this));
        //states.Add(BattleState.enemy_act, new StateEnemyAct(this));

        //有继承mono的类
        states.Add(BattleState.choose_attack_obj, stateChooseAttackObj);
        states.Add(BattleState.enemy_act, stateEnemyAct);

        states.Add(BattleState.battle_end_win, stateBattleEndWin);
        states.Add(BattleState.battle_end_lose, stateBattleEndLose);
        #endregion
        currentState = states[BattleState.battle_start];
        currentState.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        chosenGrid = factory.GetChosenGrid();
        chosenUnit = factory.GetChosenUnit();
        currentState.OnUpdate();
        Debug.Log("CURRENT_STATE:" + (int)currentStateType);
    }
    /// <summary>
    /// 状态结束后默认调用此方法，进行状态间的过渡
    /// </summary>
    public virtual void TransitionState()
    {

    }
    /// <summary>
    /// 转换为指定状态
    /// </summary>
    /// <param name="type">指定状态</param>
    public virtual void TransitionState(BattleState type)
    {
        //如果当前存在状态，则进入当前状态的OnExit方法
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[type];//切换当前状态
        float cooling_time = 0.2f; //s
        Invoke("_TransitionState", cooling_time);//等一会再进入下个阶段，否则可能连续读取输入

    }
    public virtual void _TransitionState()
    {
        currentState.OnEnter();//执行切换后状态的OnEnter方法
    }
    public void CloneAttackUnit()
    {
        factory.CloneUnitAvatar(attackUnit);
    }
    public void DistroyCloneAttackUnit()
    {
        factory.DistroyCloneUnitAvatar(attackUnit);
    }
    public void MoveAttackUnitPos2CloneAndDistroyClone()
    {
        factory.UnitMove2ClonePos(attackUnit);
        DistroyCloneAttackUnit();
    }
    public bool AttackUnitTryReach()
    {
        return factory.UnitDisplayedMove(attackUnit, chosenGrid);
    }
    public bool AttackUnitCanReach()
    {
        return factory.UnitReachGrid(attackUnit, chosenGrid);
    }
    public void SetAttackUnitLastPos()
    {
        lastPos = attackUnit.GetPos();
    }
    public void ResetAttackUnitLastPos()
    {
        lastPos = null;
    }
    public void AttackUnitBackTrack()
    {
        if (lastPos != null)
            factory.UnitReachGrid(attackUnit, lastPos);
    }
    public void SetAttackUnit()
    {
        attackUnit = factory.GetChosenUnit();
        SetAttackUnitLastPos();
    }
    public void ResetAttackUnit()
    {
        attackUnit = null;
        ResetAttackUnitLastPos();
    }
    public void SetDefenceUnit()
    {
        defenceUnit = factory.GetChosenUnit();
    }
    public void ResetDefenceUnit()
    {
        defenceUnit = null;
    }
    public void UnitsBattle()
    {
        if (attackUnit && defenceUnit)
        {
            factory.UnitsBattle(attackUnit, defenceUnit);
        }
    }
    public bool IsChosenUnit()
    {
        return factory.GetChosenUnit() != null;
    }
    public bool IsBattleWin()
    {
        //敌人数为零则获胜
        if (factory.CountEnemyLived() != 0)
        {
            return false;
        }
        else return true;
    }
    public bool IsBattleLose()
    {
        //己方人数为零则失败
        if (factory.CountMyArmyLived() != 0)
        {
            return false;
        }
        else return true;
    }
    public void AttackUnitMoved()
    {
        attackUnit.IsMoved = true;
    }
    public bool IsAllMyArmyMoved()
    {
        if (factory.unitManager.CountArmyUnitUnmoved(myArmyIndex) == 0)
        {
            return true;
        }
        else return false;
    }
    public void EnemyAttack_()
    {
        RoleUnit[] roles = factory.unitManager.units;
        foreach (RoleUnit role in roles)
        {
            if (!role.isDead && !factory.unitManager.IsUnitMyArmy(role))
            {
                RoleUnit obj_role = factory.unitManager.EnemyFindAttackObject(role);
                factory.gridManager.RoleFindPathAndMove(role, obj_role);
            }
        }
        //TransitionState(BattleState.choose_unit);
    }
    public IEnumerator EnemyAttack()
    {
        RoleUnit[] roles = factory.unitManager.units;
        foreach (RoleUnit role in roles)
        {
            if (!role.isDead && !factory.unitManager.IsUnitMyArmy(role))
            {
                RoleUnit obj_role = factory.unitManager.EnemyFindAttackObject(role);
                factory.gridManager.RoleFindPathAndMove(role, obj_role);
                yield return new WaitForSeconds(0.2f);
            }
        }
        //TransitionState(BattleState.choose_unit);
    }
}
public enum BattleState
{
    battle_start,
    //checkpoint,
    choose_unit,
    choose_move_pos,
    choose_attack_obj,
    enemy_act,
    battle_end_win,
    battle_end_lose
};