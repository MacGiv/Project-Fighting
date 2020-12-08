using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinisherStatePKC : PlayerAttackState
{
    private int hitCounter;
    private int maxHits;

    public PlayerFinisherStatePKC(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hitCounter = 1;
        maxHits = playerData.pKCFinisherHits;
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
    }

    public override void Exit()
    {
        base.Exit();
        player.comboHandler.CannotFinisher();
        player.comboHandler.ResetComboAll();
    }

    public override void LogicUpdate()
    {
        _xInput = player.InputHandler.NormalizedInputX;
        base.LogicUpdate();
    }

    

    public override void PhysicsUpdate() { }

    public override void CheckEnemyHitbox()
    {
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        if (_collidersDetected.Length != 0)
        {
            foreach (Collider2D colliderDetected in _collidersDetected)
            {
                ICanHandleFinishers canBeHit = colliderDetected.GetComponent<ICanHandleFinishers>();
                if (canBeHit != null)
                {
                    if (hitCounter <= 1)
                    {
                        canBeHit.HandlePKCFinisher(player.playerMovement.FacingDirection);
                        player.vfxHandler.PlayNormalHitVFX();

                        hitCounter++;
                    }
                    else if (hitCounter == maxHits)
                    {
                        colliderDetected.GetComponent<EnemyBrain>().AnimationTrigger();

                        player.vfxHandler.PlayNormalHitVFX();

                        colliderDetected.GetComponent<ICanHandleNormalHits>().HandleGroundedNormalHit(player.playerMovement.FacingDirection * 2);

                    }
                    else
                    {
                        player.vfxHandler.PlayNormalHitVFX();

                        hitCounter++;
                    }
                }
            }
        }
    }

    public override void StateFinishedCheck()
    {
        if (isAnimationFinished)
        {
            if (!player.CheckIfGrounded())
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if (_xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (_xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }

    public override void Move()
    {
        player.playerMovement.SetVelocityX(playerData.attackVelocity * 1.5f * player.playerMovement.FacingDirection);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
