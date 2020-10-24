using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboPostDashState : PlayerState
{
    public PlayerComboPostDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.StopAllMovement();
    }

    public override void Exit()
    {
        base.Exit();
        player.comboHandler.lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (player.CheckIfEnemyInRange() && player.InputHandler.AttackInput)
            {
                Debug.Log("POST Dash ENEMY IN RANGE");

                player.comboHandler.CanSecondCombo();

                stateMachine.ChangeState(player.GroundedAttackState);
            }
            else
                stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
