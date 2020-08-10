using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.SetVelocityY(playerData.jumpVelocity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!player.CheckIfGrounded())
            isAbilityDone = true;

    }
}
