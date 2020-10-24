using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public EnemyStateMachine StateMachine { get; private set; }

    #region Cached Components

    public EnemyMovement enemyMovement;
    public EnemyData enemyData;
    public EnemyHitHandler hitHandler;
    #endregion


    public EnemyIdleState IdleState { get; private set; }
    public ReceiveNormalHitState ReceiveNormalHitSt { get; private set; }
    public ReceivePushHitState ReceivePushHitSt { get; private set; }
    public ReceiveToAirHitState ReceiveToAirHitSt { get; private set; }
    public ReceiveAirHitState ReceiveAirHitSt { get; private set; }

    [SerializeField]
    private Transform _wallCheck;

    void Awake()
    {
        StateMachine = new EnemyStateMachine();

        enemyMovement = GetComponent<EnemyMovement>();
        hitHandler = GetComponent<EnemyHitHandler>();

        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        ReceiveNormalHitSt = new ReceiveNormalHitState(this, StateMachine, enemyData, "receiveNormalHit");
        ReceivePushHitSt = new ReceivePushHitState(this, StateMachine, enemyData, "receivePushHit");
        ReceiveToAirHitSt = new ReceiveToAirHitState(this, StateMachine, enemyData, "receiveToAirHit");
        ReceiveAirHitSt = new ReceiveAirHitState(this, StateMachine, enemyData, "receiveAirHit");
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }


    public bool CheckForWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * -enemyMovement.FacingDirection, enemyData.wallCheckDistance, enemyData.groundLayer);
    }
}
