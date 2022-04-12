using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyBrain enemyBrain;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected float startTime;

    protected string _animBoolName;

    public EnemyState(EnemyBrain enemyBrain, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemyBrain = enemyBrain;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this._animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        //TO DO: enemyBrain.Anim.SetBool(_animBoolName, true);
        startTime = Time.time;
        Debug.Log("Enemy entered " + stateMachine.CurrentState);
    }

    public virtual void Exit()
    {
        //TO DO: enemyBrain.Anim.SetBool(_animBoolName, true);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void AnimationTrigger()
    {

    }
}
