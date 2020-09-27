using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboDashState : PlayerState
{
    private float _dashTimeLeft;


    public PlayerComboDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        _dashTimeLeft = playerData.comboDashDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _dashTimeLeft -= Time.deltaTime;
        player.playerMovement.SetVelocityX(playerData.dashVelocity * player.playerMovement.FacingDirection);
        player.playerMovement.SetVelocityY(0f);

        if (player.CheckIfEnemyInRange() || _dashTimeLeft <= 0)
        {
            if (player.CheckIfEnemyInRange())
                Debug.Log("Combo Dash ENEMY IN RANGE");

            stateMachine.ChangeState(player.ComboPostDashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
