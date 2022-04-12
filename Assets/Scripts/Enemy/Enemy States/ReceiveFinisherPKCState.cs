using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveFinisherPKCState : EnemyState
{
    private bool _finished;

    public ReceiveFinisherPKCState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemyBrain, stateMachine, enemyData, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _finished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_finished)
        {
            if (!enemyBrain.CheckForWall())
                enemyBrain.enemyMovement.StickToThePlayerOnX(enemyBrain.hitHandler.CurrentPlayerFacingDirection);
        }
        else
        {
            stateMachine.ChangeState(enemyBrain.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTrigger()
    {
        _finished = true;
    }
    
}
