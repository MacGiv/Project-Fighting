using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedAttackState : PlayerState
{
    private float _xInput;
    private Collider2D[] _collidersDetected;
    private int currentComboType;

    

    public PlayerGroundedAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        StillSameCombo();
        SetAnimatorCombo();
    }

    private void SetAnimatorCombo()
    {
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
        player.comboHandler.CheckIfComboLost();
        player.Anim.SetFloat("comboTracker", player.comboHandler.comboTracker);
    }

    private void StillSameCombo()
    {
        currentComboType = player.comboHandler.GetAttackInputPressedType();
        if (player.comboHandler.lastComboTypePressed != currentComboType)
            player.comboHandler.ResetComboAll();
    }

    public override void Exit()
    {
        base.Exit();

        player.comboHandler.lastComboTypePressed = currentComboType;
        player.comboHandler.lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = player.InputHandler.NormalizedInputX;

        WallAheadCheck();

        StateFinishedCheck();
    }

    private void WallAheadCheck()
    {
        if (!player.TouchingWallInCombo())
            player.playerMovement.SetVelocityX(playerData.attackVelocity * player.playerMovement.FacingDirection);
        else
            player.playerMovement.StopAllMovement();
    }

    private void StateFinishedCheck()
    {
        if (isAnimationFinished && _xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (isAnimationFinished && _xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckEnemyHitbox()
    {
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        if (_collidersDetected.Length != 0)
        {
            if (player.comboHandler.comboTracker < 4)
            {
                foreach (Collider2D colliderDetected in _collidersDetected)
                {
                    ICanHandleNormalHits canBeHit = colliderDetected.gameObject.GetComponent<ICanHandleNormalHits>();
                    if (canBeHit != null)
                    {
                        canBeHit.HandleGroundedNormalHit(player.playerMovement.FacingDirection);

                        player.vfxHandler.PlayNormalHitVFX();

                        player.comboHandler.comboTracker++;

                        if (player.comboHandler.comboTracker == 4)
                        {
                            if (!player.comboHandler.SecondCombo)
                                player.comboHandler.CanChain();
                            else
                                player.comboHandler.CanFinisher();
                        }
                    }
                }
            }
        }
        else
        {
            player.comboHandler.ResetComboAll();
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

}
