using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
        _amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetJumping();
    }

    public bool CanJump()
    {
        if (_amountOfJumpsLeft > 0)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
}
