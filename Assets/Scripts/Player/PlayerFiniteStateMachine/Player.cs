using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerGroundedAttackState GroundedAttackState { get; private set; }
    public PlayerChainAttackState ChainState { get; private set; }
    public PlayerToAirAttackState ToAirAttackState { get; private set; }
    public PlayerComboJumpState ComboJumpState { get; private set; }
    public PlayerComboPreDashState ComboPreDashState { get; private set; }
    public PlayerComboDashState ComboDashState { get; private set; }
    public PlayerComboPostDashState ComboPostDashState { get; private set; }
    public PlayerAirAttackState AirAttackState { get; private set; }
    public PlayerPushAttackState PushAttackState { get; private set; }
    public PlayerGetFinisherState GetFinisherState { get; private set; }
    public PlayerFinisherStatePKC FinisherStatePKC { get; private set; }
    #endregion

    #region Components

    public PlayerMovement playerMovement { get; private set; }
    public PlayerComboHandler comboHandler { get; private set; }
    public PlayerJump playerJump { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _enemyInAirRange;
 
    public Transform hitCheck;
    #endregion

    #region Other Variables

    [SerializeField]
    private PlayerData _playerData;
    public EnemyBrain currentEnemyBrain;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, _playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, _playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _playerData, "wallSliding");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, _playerData, "inAir");
        DashState = new PlayerDashState(this, StateMachine, _playerData, "dash");
        GroundedAttackState = new PlayerGroundedAttackState(this, StateMachine, _playerData, "groundedAttack");
        ChainState = new PlayerChainAttackState(this, StateMachine, _playerData, "chainState"); //MMMMMMMMMMM
        ToAirAttackState = new PlayerToAirAttackState(this, StateMachine, _playerData, "toAirAttack");
        ComboJumpState = new PlayerComboJumpState(this, StateMachine, _playerData, "comboJump");
        ComboPreDashState = new PlayerComboPreDashState(this, StateMachine, _playerData, "comboPreDash");
        ComboDashState = new PlayerComboDashState(this, StateMachine, _playerData, "comboDash");
        ComboPostDashState = new PlayerComboPostDashState(this, StateMachine, _playerData, "comboPostDash");
        AirAttackState = new PlayerAirAttackState(this, StateMachine, _playerData, "airAttack");
        PushAttackState = new PlayerPushAttackState(this, StateMachine, _playerData, "pushAttack");
        GetFinisherState = new PlayerGetFinisherState(this, StateMachine, _playerData, "getFinisher");
        FinisherStatePKC = new PlayerFinisherStatePKC(this, StateMachine, _playerData, "finisher");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        comboHandler = GetComponent<PlayerComboHandler>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        //Debug.Log("Is Kyo grounded? " + CheckIfGrounded());
        //Debug.Log("Is Kyo touching wall? " + CheckIfTouchingWall());
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _playerData.groundCheckRadius, _playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * playerMovement.FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);
    }

    public bool EnoughHeightDistance()
    {
        return Physics2D.Raycast(_groundCheck.position, Vector2.down, _playerData.downRaycastHeight, _playerData.whatIsGround);
    }

    public bool CheckIfEnemyInRange()
    {
        return Physics2D.Raycast(_enemyInAirRange.position, Vector2.right * playerMovement.FacingDirection, _playerData.enemyInAirRangeDistance, _playerData.enemyLayer);
    }

    public EnemyBrain GetEnemyInRange()
    {
        RaycastHit2D raycast = Physics2D.Raycast(_enemyInAirRange.position, Vector2.right * playerMovement.FacingDirection, _playerData.enemyInAirRangeDistance, _playerData.enemyLayer);
        return raycast.collider.gameObject.GetComponent<EnemyBrain>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _playerData.groundCheckRadius);
        Gizmos.DrawWireSphere(hitCheck.position, _playerData.hitCkeckRadius);
    }
    #endregion

}
