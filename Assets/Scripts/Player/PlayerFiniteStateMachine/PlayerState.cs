using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected string _animBoolName;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this._animBoolName = boolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(_animBoolName, true);
        startTime = Time.time;
        Debug.Log("State entered: " + _animBoolName);

        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(_animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate() { }
    

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }


    //public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;

    public virtual void CheckEnemyHitbox() { }
}
