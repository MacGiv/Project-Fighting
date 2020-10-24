using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveToAirHitState : EnemyState
{
    float durationTimer;

    public ReceiveToAirHitState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        durationTimer = enemyData.toAirHitDuration;
        enemyBrain.enemyMovement.SetRecieveToAirHitVelocity(enemyBrain.hitHandler.CurrentPlayerFacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            stateMachine.ChangeState(enemyBrain.IdleState);  // <------------ ||  CHANGE TO InAirState  ||
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }
}
