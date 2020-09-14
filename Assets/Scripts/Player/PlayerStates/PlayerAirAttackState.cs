using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private float _xInput;
    private Collider2D[] _collidersDetected;
    private int currentComboType;

    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
