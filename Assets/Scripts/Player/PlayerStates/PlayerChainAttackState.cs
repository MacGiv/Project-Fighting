﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChainAttackState : PlayerState
{
    private int currentComboType;

    public PlayerChainAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        currentComboType = player.comboHandler.GetAttackInputPressedType();
        if (currentComboType != player.comboHandler.lastComboTypePressed)
        {
            player.comboHandler.ResetComboTracker();
        }
            

        player.comboHandler.CheckIfComboLost();
        if (player.comboHandler.comboTracker == 4)
        {
            if (player.InputHandler.AttackInputA)
            {
                CheckChainStateChange(playerData.aInputChainType);
            }
            else if (player.InputHandler.AttackInputB)
            {
                CheckChainStateChange(playerData.bInputChainType);
            }
        }
        else
            player.StateMachine.ChangeState(player.IdleState);
        

    }

    public override void Exit()
    {
        base.Exit();
        player.comboHandler.lastComboTypePressed = currentComboType;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    private void CheckChainStateChange(int chainType)
    {
        switch (chainType)
        {
            case 1:
                stateMachine.ChangeState(player.ToAirAttackState);
                break;
            case 2:
                stateMachine.ChangeState(player.PushAttackState);
                break;
            case 3:
                //TO DO: change to Stun/Counter Attack State
                break;
            default:
                stateMachine.ChangeState(player.IdleState);
                break;
        }
    }

}
