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
 
    public Transform hitCheck;
    #endregion

    #region Other Variables

    [SerializeField]
    private PlayerData _playerData;

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _playerData.groundCheckRadius);
        Gizmos.DrawWireSphere(hitCheck.position, _playerData.hitCkeckRadius);
    }
    #endregion

}
