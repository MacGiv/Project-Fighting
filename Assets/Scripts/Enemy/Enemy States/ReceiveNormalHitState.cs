using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveNormalHitState : EnemyState
{
    float durationTimer;


    public ReceiveNormalHitState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        durationTimer = enemyData.normalHitDuration;
        if (enemyBrain.hitHandler.GetPlayerCombotracker() == 1 && !enemyBrain.CheckForWall())
            enemyBrain.enemyMovement.StickToThePlayer(enemyBrain.hitHandler.CurrentPlayerFacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        enemyBrain.enemyMovement.StopAllMovement();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            stateMachine.ChangeState(enemyBrain.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!enemyBrain.CheckForWall())
            enemyBrain.enemyMovement.SetRecieveNormalHitVelocity(enemyBrain.hitHandler.CurrentPlayerFacingDirection);
    }

}
