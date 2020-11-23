using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinisherStateKOC : PlayerState
{
    private float _xInput;
    private Collider2D[] _collidersDetected;
    private int currentComboType;

    public PlayerFinisherStateKOC(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName) { }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
        StickEnemyToPlayer();
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

        if (!player.TouchingWallInCombo())
            player.playerMovement.SetDoubleDirectionalVelocity(playerData.airAttackvelocityX /2 , 0.75f);

        if (isAnimationFinished && !player.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.InAirState);
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
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

}
