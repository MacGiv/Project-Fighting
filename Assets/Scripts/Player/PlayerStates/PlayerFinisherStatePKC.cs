using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinisherStatePKC : PlayerState
{
    private float _xInput;
    private Collider2D[] _collidersDetected;
    private int currentComboType;

    public PlayerFinisherStatePKC(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetFloat("comboType", player.comboHandler.GetAttackInputPressedType());
    }

    public override void Exit()
    {
        base.Exit();
        player.comboHandler.CannotFinisher();
        player.comboHandler.ResetComboTracker();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = player.InputHandler.NormalizedInputX;
        player.playerMovement.SetVelocityX(playerData.attackVelocity * player.playerMovement.FacingDirection);

        if (isAnimationFinished && !player.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isAnimationFinished && _xInput == 0)
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
        base.CheckEnemyHitbox();
        _collidersDetected = Physics2D.OverlapCircleAll(player.hitCheck.position, playerData.hitCkeckRadius, playerData.enemyLayer);

        //Debug.Log("I've entered in the CheckEnemyHitbox of the GROUNDED ATTACK STATE");

        if (_collidersDetected.Length != 0)
        {
            foreach (Collider2D colliderDetected in _collidersDetected)
            {
                
            }
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
