using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyGroundedState, IChainHittable
{
    public EnemyIdleState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
    {
    }

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
    }

    public override void RecieveGroundedNormalHit(int playerFacingDirection)
    {
        base.RecieveGroundedNormalHit(playerFacingDirection);
    }

    public override void RecieveToAirHit(int playerFacingDirection)
    {
        base.RecieveToAirHit(playerFacingDirection);
    }

    public override void RecievePushHit() { }
    public override void RecieveStunHit() { }
}
