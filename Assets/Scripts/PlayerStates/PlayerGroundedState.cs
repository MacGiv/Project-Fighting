﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool _jumpInput;

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

        if (_jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.JumpInputWasUsed();
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
