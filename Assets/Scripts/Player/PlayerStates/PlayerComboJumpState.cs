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
        player.JumpState.DecreaseAmountOfJumpsLeft();
        enemyCheckDelay = playerData.enemyCheckDelay;
        player.playerMovement.SetDoubleDirectionalVelocity(playerData.comboJumpVelocityX, playerData.comboJumpVelocityY);

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

        if (player.InputHandler.AttackInput)
        {
            if (player.CheckIfEnemyInRange())
            {
                player.currentEnemyBrain = player.GetEnemyInRange();
                player.currentEnemyBrain.RecievingAirComboTrue();

                player.comboHandler.CanPerformAirCombo();

                stateMachine.ChangeState(player.AirAttackState);
            }
        }
        else if (enemyCheckDelay <= 0)
        {
            stateMachine.ChangeState(player.InAirState);
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
