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
    public ReceivePushDownHitState ReceivePushDownHitSt { get; private set; }
    public ReceiveAirHitState ReceiveAirHitSt { get; private set; }
    public ReceiveFinisherPKCState ReceiveFinisherPKCSt { get; private set; }
    public ReceiveFinisherKOCState ReceiveFinisherKOCSt { get; private set; }

    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _groudCheck;

    void Awake()
    {
        StateMachine = new EnemyStateMachine();

        enemyMovement = GetComponent<EnemyMovement>();
        hitHandler = GetComponent<EnemyHitHandler>();

        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        ReceiveNormalHitSt = new ReceiveNormalHitState(this, StateMachine, enemyData, "receiveNormalHit");
        ReceivePushHitSt = new ReceivePushHitState(this, StateMachine, enemyData, "receivePushHit");
        ReceiveToAirHitSt = new ReceiveToAirHitState(this, StateMachine, enemyData, "receiveToAirHit");
        ReceivePushDownHitSt = new ReceivePushDownHitState(this, StateMachine, enemyData, "receivePushDownHit");
        ReceiveAirHitSt = new ReceiveAirHitState(this, StateMachine, enemyData, "receiveAirHit");
        ReceiveFinisherPKCSt = new ReceiveFinisherPKCState(this, StateMachine, enemyData, "receiveFinisherPKC");
        ReceiveFinisherKOCSt = new ReceiveFinisherKOCState(this, StateMachine, enemyData, "receiveFinisherKOC");
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

    public void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }


    public bool IsTouchingGround()
    {
        return Physics2D.OverlapCircle(_groudCheck.position, enemyData.groundCheckRadius, enemyData.groundLayer);
    }

    public bool CheckForWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * -enemyMovement.FacingDirection, enemyData.wallCheckDistance, enemyData.groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_wallCheck.position, Vector2.right * -GetComponent<EnemyMovement>().FacingDirection * enemyData.wallCheckDistance);
    }
}
