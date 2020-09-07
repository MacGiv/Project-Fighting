using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundedState : EnemyState, INormalHittable, IChainHittable
{


    public EnemyGroundedState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
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

    public virtual void RecieveGroundedNormalHit(int playerFacingDirection)
    {
        enemyBrain.EnemyMovement.SetRecieveNormalHitVelocity(playerFacingDirection);
    }

    public virtual void RecieveToAirHit(int playerFacingDirection)
    {
        enemyBrain.EnemyMovement.SetRecieveToAirHitVelocity(playerFacingDirection);
    }

    public virtual void RecievePushHit() { }
    public virtual void RecieveStunHit() { }

}
