using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int _xInput;
    private bool _isGrounded;
    private bool _coyoteTime;
    private bool _jumpInput;

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
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        _xInput = player.InputHandler.NormalizedInputX;
        _isGrounded = player.CheckIfGrounded();
        _jumpInput = player.InputHandler.JumpInput;

        if (_isGrounded && player.playerMovement.RB.velocity.y < 0.1f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (_jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            player.playerMovement.CheckIfShouldFlip(_xInput);
            player.playerMovement.SetVelocityX(playerData.movementVelocity * _xInput);

            player.Anim.SetFloat("yVelocity", player.playerMovement.RB.velocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.RB.velocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            _coyoteTime = false;
            player.InputHandler.JumpInputWasUsed();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

}
