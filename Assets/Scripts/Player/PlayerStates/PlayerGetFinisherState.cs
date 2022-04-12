using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetFinisherState : PlayerState
{
    private int currentComboType;

    public PlayerGetFinisherState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string boolName) : base(player, stateMachine, playerData, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        currentComboType = player.comboHandler.GetAttackInputPressedType();
        if (currentComboType != player.comboHandler.lastComboTypePressed)
        {
            player.comboHandler.ResetComboTracker();
        }


        player.comboHandler.CheckIfComboLost();
        if (player.comboHandler.comboTracker == 4)
        {
            if (player.InputHandler.AttackInputA)
            {
                CheckFinisherStateChange(playerData.aInputComboType);
            }
            else if (player.InputHandler.AttackInputB)
            {
                CheckFinisherStateChange(playerData.bInputComboType);
            }
        }
        else
            player.StateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public void CheckFinisherStateChange(int finisherType)
    {
        switch (finisherType)
        {
            case 1:
                stateMachine.ChangeState(player.FinisherStatePKC);
                break;
            case 2:
                //Change state to Finisher KOC
                break;
            case 3:
                //Change state to Finisher AAC
                break;
            default:
                stateMachine.ChangeState(player.IdleState);
                break;
        }
    }
}
