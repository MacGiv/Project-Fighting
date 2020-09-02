using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedAttackState : PlayerState
{
    private float _xInput;
    private Collider2D[] _collidersDetected;

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
        player.comboHandler.CheckIfComboLost();
        player.Anim.SetFloat("comboTracker", player.comboHandler.comboTracker);
    }

    public override void Exit()
    {
        base.Exit();
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

    public void CheckEnemyHitbox()
    {
        //TO DO: Check if the player has hit an enemy
        //TO DO: If has hit an enemy comboTracker++

        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        if (_collidersDetected.Length != 0)
        {
            Debug.Log("COLLIDERS DETECTED " + _collidersDetected.Length);

            if (player.comboHandler.comboTracker < 4)
            {
                foreach (Collider2D colliderDetected in _collidersDetected)
                {
                    Debug.Log("Collider's name: " + colliderDetected.gameObject.name);

                    EnemyBrain enemyBrainDetected = colliderDetected.GetComponent<EnemyBrain>();
                    enemyBrainDetected.HandleGroundedNormalHit(player.playerMovement.FacingDirection);
                }

                player.comboHandler.comboTracker++;

                Debug.Log("ComboTracker Value: " + player.comboHandler.comboTracker);
            }
            
        }

        
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        //If the animation ended then continue combo or change state

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
