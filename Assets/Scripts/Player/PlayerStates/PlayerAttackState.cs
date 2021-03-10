using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    protected float _xInput;
    protected Collider2D[] _collidersDetected;
    protected int currentComboType;

    private bool _hittingAir;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }


    

    public override void Enter()
    {
        base.Enter();
        _hittingAir = IsHittingAir();
        StillSameCombo();
        SetAnimatorCombo();
    }

    public virtual void SetAnimatorCombo()
    {
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
        player.comboHandler.CheckIfComboLost();
        player.Anim.SetFloat("comboTracker", player.comboHandler.comboTracker);
    }

    public void StillSameCombo()
    {
        currentComboType = player.comboHandler.GetAttackInputPressedType();
        if (player.comboHandler.lastComboTypePressed != currentComboType)
            player.comboHandler.ResetComboAll();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        MoveIfNotWallAhead();
        StateFinishedCheck();
    }

    public virtual void MoveIfNotWallAhead()
    {
        if (!player.TouchingWallInCombo() && !_hittingAir)
            Move();
        else
            player.playerMovement.StopAllMovement();
    }

    public virtual void StateFinishedCheck() { }  //Has to be overritten
    public virtual void Move() { }                //Has to be overritten

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckEnemyHitbox()
    {
        base.CheckEnemyHitbox();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    private bool IsHittingAir()
    {
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        if (_collidersDetected.Length == 0)
            return true;
        else
            return false;
    }
}
