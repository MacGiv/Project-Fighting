using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        //TO DO: Ejecutar el código de abajo si termino la animación 
        if (xInput == 0)
            player.StateMachine.ChangeState(player.IdleState);
        else
            player.StateMachine.ChangeState(player.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
