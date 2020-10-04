using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyMovement EnemyMovement { get; private set; }

    public EnemyIdleState IdleState { get; private set; }

    public EnemyData _enemyData;

    public bool recieveingAirCombo = false;

    void Awake()
    {
        StateMachine = new EnemyStateMachine();

        EnemyMovement = GetComponent<EnemyMovement>();

        IdleState = new EnemyIdleState(this, StateMachine, _enemyData, "idle");
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public void HandleGroundedNormalHit(int playerFacingDirection)
    {
        if (StateMachine.CurrentState == IdleState)
        {
            IdleState.RecieveGroundedNormalHit(playerFacingDirection);
        }
    }

    public void HandleToAirHit(int playerFacingDirection)
    {
        if (StateMachine.CurrentState == IdleState)
        {
            IdleState.RecieveToAirHit(playerFacingDirection);
        }
    }

    public void HandlePushHit(int playerFacingDirection)
    {
        if (StateMachine.CurrentState == IdleState)
        {
            IdleState.RecievePushHit(playerFacingDirection);
        }
    }


    public void RecievingAirComboTrue() => recieveingAirCombo = true;
    public void RecievingAirComboFalse() => recieveingAirCombo = false;

    

}
