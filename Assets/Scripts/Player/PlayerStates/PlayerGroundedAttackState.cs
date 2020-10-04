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

        currentComboType = player.comboHandler.GetAttackInputPressedType();
        if (player.comboHandler.lastComboTypePressed != currentComboType)
            player.comboHandler.ResetComboTracker();

        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
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

        _xInput = player.InputHandler.NormalizedInputX;

        player.playerMovement.SetVelocityX(playerData.attackVelocity * player.playerMovement.FacingDirection);

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

        //Debug.Log("I've entered in the CheckEnemyHitbox of the GROUNDED ATTACK STATE");

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

                if (player.comboHandler.comboTracker >= 4)
                {
                    if (!player.comboHandler.SecondCombo)
                        player.comboHandler.CanChain();
                    else
                        player.comboHandler.CanFinisher();
                }
                //Debug.Log("ComboTracker Value: " + player.comboHandler.comboTracker);
            }
        }

        
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
