using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion

    #region Components

    public PlayerMovement playerMovement { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other Variables

    [SerializeField]
    private PlayerData _playerData;
    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    
}
