using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToAirAttackState : PlayerAttackState
{

    public PlayerToAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
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
        player.comboHandler.lastChainAttackTime = Time.time;
        player.Anim.SetFloat("comboTracker", player.comboHandler.comboTracker);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = player.InputHandler.NormalizedInputX;
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


    public override void Move()
    {
        player.playerMovement.SetVelocityX(playerData.attackVelocityKOC * player.playerMovement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckEnemyHitbox()
    {
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        //Debug.Log("I've entered in the CheckEnemyHitbox of the TOAIR STATE");

        if (_collidersDetected.Length != 0)
        {
            foreach (Collider2D colliderDetected in _collidersDetected)
            {
                ICanHandleSpecialHits canBeChainHitted = colliderDetected.gameObject.GetComponent<ICanHandleSpecialHits>();
                if (canBeChainHitted != null)
                {
                    canBeChainHitted.HandleToAirHit(player.playerMovement.FacingDirection);

                    player.vfxHandler.PlayNormalHitVFX();
                }
                else
                    Debug.Log("NO IChainHittable Found in " + colliderDetected.gameObject.name);
            }
        }
    }


    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
