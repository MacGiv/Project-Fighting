using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUniqueAttackState : PlayerAttackState
{
    public PlayerUniqueAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void CheckEnemyHitbox()
    {
        base.CheckEnemyHitbox();
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

    public override void Move()
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetAnimatorCombo()
    {
        base.SetAnimatorCombo();
    }

    public override void StateFinishedCheck()
    {
        base.StateFinishedCheck();
    }

    public override void MoveIfNotWallAhead()
    {
        base.MoveIfNotWallAhead();
    }

    
}
