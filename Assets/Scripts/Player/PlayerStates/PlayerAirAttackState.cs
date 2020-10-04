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

    public override void Enter()
    {
        base.Enter();

        currentComboType = player.comboHandler.GetAttackInputPressedType();
        if (currentComboType != player.comboHandler.lastComboTypePressed)
            player.comboHandler.ResetComboTracker();

        isAnimationFinished = false;
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
        if (player.comboHandler.comboTracker != 1)
            player.comboHandler.CheckIfComboLost();
        player.Anim.SetFloat("comboTracker", player.comboHandler.comboTracker);
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

        player.playerMovement.SetDoubleDirectionalVelocity(playerData.airAttackvelocityX, 0f);

        if (isAnimationFinished)
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
            if (player.comboHandler.comboTracker < 4)
            {
                foreach (Collider2D colliderDetected in _collidersDetected)
                {
                    ICanHandleHits canBeHit = colliderDetected.gameObject.GetComponent<ICanHandleHits>();
                    if (canBeHit != null)
                    {
                        EnemyBrain enemyBrainDetected = colliderDetected.GetComponent<EnemyBrain>();
                        enemyBrainDetected.HandleGroundedNormalHit(player.playerMovement.FacingDirection);
                    }
                }

                player.comboHandler.comboTracker++;

                if (player.comboHandler.comboTracker == 4)
                {
                    player.comboHandler.CanFinisher();
                }
            }
        }
        player.comboHandler.comboTracker++;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
