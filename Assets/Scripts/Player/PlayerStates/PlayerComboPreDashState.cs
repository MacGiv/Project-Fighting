using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboPreDashState : PlayerState
{
    public PlayerComboPreDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.playerMovement.StopAllMovement();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.ComboDashState);
        } 
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
