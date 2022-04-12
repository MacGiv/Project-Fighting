using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }


    public override void Enter()
    {
        isAnimationFinished = false;
        base.Enter();
    }

    public override void Exit()
    {
        player.playerMovement.ClimbLedge();
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished)
        {
            player.playerMovement.StopAllMovement();
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
