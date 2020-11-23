using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePushDownHitState : EnemyState
{
    public ReceivePushDownHitState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!enemyBrain.IsTouchingGround())
            enemyBrain.enemyMovement.SetReceivePushDownHitVelocity();
        else
            enemyBrain.StateMachine.ChangeState(enemyBrain.IdleState);
    }
}
