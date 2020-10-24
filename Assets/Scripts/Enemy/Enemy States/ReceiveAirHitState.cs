using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveAirHitState : EnemyState
{
    float durationTime;

    public ReceiveAirHitState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Im an enemy, and i've entered in RECEIVE AIR HIT STATE");
        durationTime = enemyData.airHitDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        durationTime -= Time.deltaTime;
        if (durationTime <= 0)
        {
            enemyBrain.enemyMovement.SetDoubleDirectionalVelocity(enemyBrain.hitHandler.CurrentPlayerFacingDirection, 2f, -1f);
            stateMachine.ChangeState(enemyBrain.IdleState);
        }
        else
            enemyBrain.enemyMovement.StickToThePlayer(enemyBrain.hitHandler.CurrentPlayerFacingDirection * 1.3f);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
