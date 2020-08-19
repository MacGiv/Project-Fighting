using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState: PlayerState
{
    private float _xInput;
    private float _yRawInput;

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    { }

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
        player.InAirState.StartCoyoteTime();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = player.InputHandler.NormalizedInputX;
        _yRawInput = player.InputHandler.RawMovementInput.y;

        player.playerMovement.SetVelocityY(playerData.wallSlideVelocity);

        if (!isExitingState)
        {
            if (player.InputHandler.JumpInput && _xInput != player.playerMovement.FacingDirection && player.InputHandler.MoveInputStarted)
            {
                stateMachine.ChangeState(player.WallJumpState);
            }

            if (player.CheckIfGrounded())
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if (!player.CheckIfTouchingWall() || (_yRawInput < -0.8f && _xInput != player.playerMovement.FacingDirection ))
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
