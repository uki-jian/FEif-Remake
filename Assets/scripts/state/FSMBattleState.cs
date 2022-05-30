using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBattleState : FSM
{
    public IState currentState;//��ǰ״̬
    public BattleStateType currentStateType;//��ǰ״̬����
    public Dictionary<BattleStateType, IState> states = new Dictionary<BattleStateType, IState>();//״̬�ֵ�
    public FactoryOfUnitAndGrid factory;

    public UnitClass attackUnit;//������λ
    public UnitClass defenceUnit;//���ص�λ

    public UnitClass chosenUnit;
    public GridClass chosenGrid;

    Pos lastPos;
    int myArmyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        #region
        states.Add(BattleStateType.battle_start, new StateBattleStart(this));
        states.Add(BattleStateType.choose_unit, new StateChooseUnit(this));
        states.Add(BattleStateType.choose_move_pos, new StateChosenMovePos(this));
        states.Add(BattleStateType.choose_attack_obj, new StateChooseAttackObj(this));
        states.Add(BattleStateType.enemy_choose_unit, new StateEnemyAct(this));
        states.Add(BattleStateType.battle_end_win, new StateBattleEndWin(this));
        states.Add(BattleStateType.battle_end_lose, new StateBattleEndLose(this));
        #endregion
        currentState = states[BattleStateType.battle_start];
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
    /// ״̬������Ĭ�ϵ��ô˷���������״̬��Ĺ���
    /// </summary>
    public virtual void TransitionState()
    {

    }
    /// <summary>
    /// ת��Ϊָ��״̬
    /// </summary>
    /// <param name="type">ָ��״̬</param>
    public virtual void TransitionState(BattleStateType type)
    {
        //�����ǰ����״̬������뵱ǰ״̬��OnExit����
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[type];//�л���ǰ״̬
        float cooling_time = 0.5f; //s
        Invoke("_TransitionState", cooling_time);//��һ���ٽ����¸��׶Σ��������������ȡ����

    }
    public virtual void _TransitionState()
    {
        currentState.OnEnter();//ִ���л���״̬��OnEnter����
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
        //������Ϊ�����ʤ
        if (factory.CountEnemyLived() != 0)
        {
            return false;
        }
        else return true;
    }
    public bool IsBattleLose()
    {
        //��������Ϊ����ʧ��
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
}
public enum BattleStateType
{
    battle_start,
    checkpoint,
    choose_unit,
    choose_move_pos,
    choose_attack_obj,
    enemy_checkpoint,
    enemy_choose_unit,
    enemy_choose_move_pos,
    enemy_choose_attack_obj,
    battle_end_win,
    battle_end_lose
};