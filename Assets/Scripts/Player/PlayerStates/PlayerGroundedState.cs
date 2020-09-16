﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool _jumpInput;
    private bool _dashInput;
    private bool _attackInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormalizedInputX;
        _jumpInput = player.InputHandler.JumpInput;
        _dashInput = player.InputHandler.DashInput;
        _attackInput = player.InputHandler.AttackInput;

        if (_attackInput)
        {
            if (player.comboHandler.comboTracker >= 4)
            {
                stateMachine.ChangeState(player.ChainState);
            }
            else
                stateMachine.ChangeState(player.GroundedAttackState);
        }

        if (_dashInput && player.DashState.CanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if (_jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.JumpInputWasUsed();
            player.comboHandler.CheckIfChainLost();

            if (player.comboHandler.CanChainCombo)
            {
                stateMachine.ChangeState(player.ComboJumpState);
            }
            else
                stateMachine.ChangeState(player.JumpState);
        }
        else if (!player.CheckIfGrounded())
        {
            player.InAirState.StartCoyoteTime();

            stateMachine.ChangeState(player.InAirState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}