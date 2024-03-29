﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded;
    private bool _coyoteTime;

    private int _xInput;
    private bool _jumpInput;
    private bool _isJumpingUp;
    private bool _jumpInputStop;
    private bool _dashInput;
    private bool _attackInput;


    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = player.CheckIfGrounded();        
    }

    public override void Enter()
    {
        base.Enter();
        player.comboHandler.CheckIfComboLost(); //Used to prevent the player from performing an AirCombo if is not after a ComboJump
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        GetInput();

        CheckJumpMultiplier();

        if (_attackInput && player.comboHandler.CanAirCombo && player.comboHandler.GetAttackInputPressedType() == (int)ComboTypeIndexes.KICKS_ONLY)
        {
            if (player.comboHandler.CanFinisherMove)
                stateMachine.ChangeState(player.FinisherStateKOC);
            else
                stateMachine.ChangeState(player.AirAttackState);
        }
        if (_jumpInput && _coyoteTime && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        if (_isGrounded && player.playerMovement.RB.velocity.y < 0.1f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (_dashInput && player.DashState.CanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if (player.CheckIfTouchingWall() && !player.IsTouchingLedge())
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (player.CheckIfTouchingWall() && _xInput == player.playerMovement.FacingDirection && !player.EnoughHeightDistance())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.playerMovement.CheckIfShouldFlip(_xInput);

            player.playerMovement.SetVelocityX(playerData.movementVelocity * _xInput);

            player.Anim.SetFloat("yVelocity", player.playerMovement.RB.velocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.RB.velocity.x));
        }
    }

    private void GetInput()
    {
        _xInput = player.InputHandler.NormalizedInputX;
        _isGrounded = player.CheckIfGrounded();
        _jumpInput = player.InputHandler.JumpInput;
        _jumpInputStop = player.InputHandler.JumpInputStop;
        _dashInput = player.InputHandler.DashInput;
        _attackInput = player.InputHandler.AttackInput;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumpingUp)
        {
            if (_jumpInputStop)
            {
                player.playerMovement.SetVelocityY(player.playerMovement.RB.velocity.y * playerData.variableJumpHeightMultiplier);
                _isJumpingUp = false;
            }
            else if (player.playerMovement.RB.velocity.y <= 0f)
            {
                _isJumpingUp = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            _coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetJumping() => _isJumpingUp = true;

}
