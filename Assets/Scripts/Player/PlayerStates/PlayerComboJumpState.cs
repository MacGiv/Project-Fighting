using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboJumpState : PlayerState
{
    private float enemyCheckDelay;

    public PlayerComboJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyCheckDelay = playerData.enemyCheckDelay;
        player.playerMovement.SetDirectionalJumpVelocity(playerData.comboJumpVelocityX, playerData.comboJumpVelocityY);

    }

    public override void Exit()
    {
        player.comboHandler.CannotChain();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemyCheckDelay -= Time.deltaTime;

        if (enemyCheckDelay <= 0)
        {
            if (player.CheckIfEnemyInRange() && player.InputHandler.AttackInput)
            {
                //Change to AirAttackState
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CheckIfEnemyInRange()
    {
        

    }
}
