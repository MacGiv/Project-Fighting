using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedAttackState : PlayerAttackState
{
    private const int _PNKCombo = (int)ComboTypeIndexes.PUNCH_N_KICKS;
    private const int _KOCCombo = (int)ComboTypeIndexes.KICKS_ONLY;


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
        switch (currentComboType)
        {
            case _PNKCombo :
                player.playerMovement.SetVelocityX(playerData.attackVelocityPKC * player.playerMovement.FacingDirection);
                break;
            case _KOCCombo:
                player.playerMovement.SetVelocityX(playerData.attackVelocityKOC * player.playerMovement.FacingDirection);
                break;
            default:
                //player.playerMovement.SetVelocityX(playerData.attackVelocity * player.playerMovement.FacingDirection);
                break;
        }
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

}
