using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerAttackState
{

    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
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
        base.LogicUpdate();
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
                    ICanHandleAirHits canBeHit = colliderDetected.gameObject.GetComponent<ICanHandleAirHits>();
                    if (canBeHit != null)
                    {
                        canBeHit.HandleAirHit(player.playerMovement.FacingDirection);

                        player.vfxHandler.PlayNormalHitVFX();

                        player.comboHandler.comboTracker++;

                        if (player.comboHandler.comboTracker == 4)
                            player.comboHandler.CanFinisher();
                    }
                    else
                    {
                        Debug.Log("PLAYER: Non ICanHandleAirHits Found in the current attack. ");
                        player.comboHandler.CannotPerformAirCombo();
                        player.comboHandler.ResetComboTracker();
                    }
                }                
            }
        }
        else
        {
            Debug.Log("PLAYER: Non Colliders hit in the current attack.");
            player.comboHandler.CannotPerformAirCombo();
            player.comboHandler.ResetComboTracker();
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void SetAnimatorCombo()
    {
        isAnimationFinished = false;
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
        if (player.comboHandler.comboTracker != 1)
            player.comboHandler.CheckIfComboLost();
        player.Anim.SetFloat("comboTracker", player.comboHandler.comboTracker);
    }
    public override void StateFinishedCheck()
    {
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
    public override void Move()
    {
        player.playerMovement.SetDoubleDirectionalVelocity(playerData.airAttackvelocityX, 0.75f);
    }
}
