using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour, ICanHandleNormalHits, ICanHandleAirHits, ICanHandleSpecialHits
{
    public EnemyBrain enemyBrain;

    public int CurrentPlayerFacingDirection { get; private set; }

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }


    public void HandleGroundedNormalHit(int playerFacingDirection)
    {
        CurrentPlayerFacingDirection = playerFacingDirection;
        enemyBrain.StateMachine.ChangeState(enemyBrain.ReceiveNormalHitSt);
    }

    public void HandleAirHit(int playerFacingDirection)
    {
        CurrentPlayerFacingDirection = playerFacingDirection;
        enemyBrain.StateMachine.ChangeState(enemyBrain.ReceiveAirHitSt);
    }

    public void HandleToAirHit(int playerFacingDirection)
    {
        CurrentPlayerFacingDirection = playerFacingDirection;
        enemyBrain.StateMachine.ChangeState(enemyBrain.ReceiveToAirHitSt);
    }

    public void HandlePushHit(int playerFacingDirection)
    {
        CurrentPlayerFacingDirection = playerFacingDirection;
        enemyBrain.StateMachine.ChangeState(enemyBrain.ReceivePushHitSt);
    }

    public void HandleStunHit(int playerFacingDirection)
    {
        
    }

    public int GetPlayerCombotracker()
    {
        return FindObjectOfType<PlayerComboHandler>().comboTracker;
    }
}
