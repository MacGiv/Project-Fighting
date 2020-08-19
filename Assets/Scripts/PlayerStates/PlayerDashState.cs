using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float _dashTimeLeft;

    public float LastDashTime { get; private set; }


    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        LastDashTime = Time.time;
        _dashTimeLeft = playerData.dashDuration;

        player.InputHandler.DashInputWasUsed();
    }

    public override void Exit()
    {
        base.Exit();
        player.playerMovement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.playerMovement.SetVelocityX(playerData.dashVelocity * player.playerMovement.FacingDirection);
        player.playerMovement.SetVelocityY(0f);

        _dashTimeLeft -= Time.deltaTime;

        if (_dashTimeLeft <= 0 || player.CheckIfTouchingWall())
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CanDash()
    {
        if (Time.time > LastDashTime + playerData.dashCooldown)
            return true;
        else
            return false;
    }
}
