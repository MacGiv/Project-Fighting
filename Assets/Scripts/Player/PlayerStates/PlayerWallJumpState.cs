using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    bool isAbilityDone;
    bool isGrounded;

    public int xInput => player.InputHandler.NormalizedInputX; 

    float abilityTimer;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.SetWallJumpVelocity(playerData.wallJumpVelocityX, playerData.wallJumpVelocityY);
        isAbilityDone = false;
        abilityTimer = playerData.wallJumpTime;
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckAbilityDone();

        if (player.CheckIfTouchingWall())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (!player.CheckIfTouchingWall() && !isAbilityDone)
        {
            player.playerMovement.CheckIfShouldFlip(xInput);
        }
        else if (isAbilityDone)
        {
            if (!player.CheckIfGrounded())
                stateMachine.ChangeState(player.InAirState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    void CheckAbilityDone()
    {
        abilityTimer -= Time.deltaTime;
        if (abilityTimer <= 0)
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
