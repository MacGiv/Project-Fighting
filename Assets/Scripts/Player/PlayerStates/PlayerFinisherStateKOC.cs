using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinisherStateKOC : PlayerAttackState
{ 
    private bool _isAlreadySticked;

    public PlayerFinisherStateKOC(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName) { }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
        _isAlreadySticked = false;
    }

    public override void Exit()
    {
        player.comboHandler.CannotFinisher();
        player.comboHandler.ResetComboAll();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void StateFinishedCheck()
    {
        if (isAnimationFinished)
        {
            if (!player.CheckIfGrounded())
                stateMachine.ChangeState(player.InAirState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_isAlreadySticked)
        {
            StickEnemyToPlayer();
        }
    }

    public override void CheckEnemyHitbox()
    {
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        if (_collidersDetected.Length != 0)
        {
            foreach (Collider2D colliderDetected in _collidersDetected)
            {
                ICanHandleSpecialHits canBeHit = colliderDetected.GetComponent<ICanHandleSpecialHits>();
                if (canBeHit != null)
                {
                    canBeHit.HandlePushDownHit();
                    player.vfxHandler.PlayDownHitVFX();
                }
            }
        }
    }

    private void StickEnemyToPlayer()
    {
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        if (_collidersDetected.Length != 0)
        {
            foreach (Collider2D colliderDetected in _collidersDetected)
            {
                ICanHandleFinishers canBeHit = colliderDetected.GetComponent<ICanHandleFinishers>();
                if (canBeHit != null)
                {
                    canBeHit.HandleKOCFinisher();
                }
            }
            _isAlreadySticked = true;
        }
    }

    public override void Move()
    {
        player.playerMovement.SetDoubleDirectionalVelocity(playerData.airAttackvelocityX / 2, 0.75f);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

}
