using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePushHitState : EnemyState
{
    float durationTimer;


    public ReceivePushHitState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        durationTimer = enemyData.pushHitDuration;
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
            enemyBrain.enemyMovement.SetRecievePushHitVelocity(enemyBrain.hitHandler.CurrentPlayerFacingDirection);
    }
}
