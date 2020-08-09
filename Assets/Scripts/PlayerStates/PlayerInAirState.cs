using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded;
    private int _xInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = player.CheckIfGrounded();        
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
        _isGrounded = player.CheckIfGrounded();
        _xInput = player.InputHandler.NormalizedInputX;

        if (_isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            player.playerMovement.CheckIfShouldFlip(_xInput);
            player.playerMovement.SetVelocityX(playerData.movementVelocity * _xInput);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
