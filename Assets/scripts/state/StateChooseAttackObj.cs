using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChooseAttackObj : MonoBehaviour, IState
{
    public FSMBattleState manager;

    public StateChooseAttackObj(FSMBattleState _manager)
    {
        manager = _manager;
    }
    public void OnEnter() //�������״̬Ӧ�ý��еķ���
    {
        Debug.Log("STATE_START: StateChooseAttackObj");
        manager.currentStateType = BattleState.choose_attack_obj; //�ÿ������ű��ĵ�ǰ״̬�ĳɸ�״̬
        //manager.factory.gridManager.ShowAttackGridsPattern(manager.chosenUnit);
        manager.factory.gridManager.SetAndChooseAttackGrid(manager.chosenUnit);

    }
    public void OnUpdate() //ά�����״̬�ķ���
    {
        if (FSM.GetConfirmation())
        {
            //ѡ��з�����
            if (!manager.factory.IsChosenUnitMyArmy())
            {
                manager.SetDefenceUnit();
                manager.UnitsBattle();
                
            }
            //ѡ�������ط������
            if (manager.IsChosenUnit())
            {
                //manager.TransitionState(BattleStateType.battle_start);
            }
            manager.AttackUnitMoved();


            //if (manager.IsBattleLose())
            //{
            //    manager.TransitionState(BattleState.battle_end_lose);
            //}
            //else if (manager.IsBattleWin())
            //{
            //    manager.TransitionState(BattleState.battle_end_win);
            //}
            if (manager.IsAllMyArmyMoved())
            {
                print("MOVEDDDDDD");
                manager.TransitionState(BattleState.enemy_act);
            }
            else
            {
                manager.TransitionState(BattleState.choose_unit);
            }
        }

        if (FSM.GetCancell())
        {
            manager.ResetDefenceUnit();
            manager.TransitionState(BattleState.choose_move_pos);
        }
        
    }
    public void OnExit() //�˳����״̬Ӧ��ִ�еķ���
    {
        Debug.Log("STATE_END: StateChooseAttackObj");
        //manager.EnemyAttack_();
        manager.factory.gridManager.ClearGridsPattern();
    }
}
