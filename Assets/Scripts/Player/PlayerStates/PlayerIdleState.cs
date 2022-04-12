using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    bool _attackInput;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.StopAllMovement();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        

        _attackInput = player.InputHandler.AttackInput;

        if (xInput != 0 && !isExitingState && player.CheckIfGrounded() && !_attackInput)
        {
            stateMachine.ChangeState(player.MoveState);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.playerMovement.StopAllMovement();
    }
}
