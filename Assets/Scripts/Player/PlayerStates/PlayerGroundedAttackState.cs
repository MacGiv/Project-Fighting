using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedAttackState : PlayerAttackState
{
    
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
        
    }

    public override void Exit()
    {
        base.Exit();

        player.comboHandler.lastComboTypePressed = currentComboType;
        player.comboHandler.lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        _xInput = player.InputHandler.NormalizedInputX;
        base.LogicUpdate();
    }


    public override void StateFinishedCheck()
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
                            {
                                player.comboHandler.CanChain();
                                player.comboHandler.CanDoChainMove();
                            }
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

    public override void Move()
    {
        player.playerMovement.SetVelocityX(playerData.attackVelocity * player.playerMovement.FacingDirection);
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

}
