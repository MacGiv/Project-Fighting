using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Controls _controls;
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool MoveInputStarted { get; private set; }
    public bool DashInput { get; private set; }
    public bool AttackInput { get; private set; }

    [SerializeField]
    private float _inputHoldTime = 0.2f;
    private float _jumInputStartTime;
    private float _dashInputStartTime;
    private float _attackInputStartTime;


    private void OnEnable()
    {
        _controls = new Controls();
    }

    private void Update()
    {
        CheckInputHoldTime();
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.started)
            MoveInputStarted = true;

        RawMovementInput = context.ReadValue<Vector2>();

        CheckForInputX();

        //NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;

        if (context.canceled)
            MoveInputStarted = false;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("DASH input button pressed!");
            DashInput = true;
            _dashInputStartTime = Time.time;
        }

    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            Debug.Log("ATTACK input button pressed!");
            AttackInput = true;
            _attackInputStartTime = Time.time;
        }
    }

    public void CheckForInputX()
    {
        if (RawMovementInput.x >= 0.5f || RawMovementInput.x <= -0.5f )
        {
            NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            
        }
        else //if (RawMovementInput.x < 0.5f && RawMovementInput.x > -0.5f)
        {
            NormalizedInputX = 0;
            
        }
        
    }


    public void JumpInputWasUsed() => JumpInput = false;
    public void DashInputWasUsed() => DashInput = false;

    private void CheckInputHoldTime()
    {
        if (Time.time >= _jumInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
        if (Time.time >= _dashInputStartTime + _inputHoldTime)
        {
            DashInput = false;
        }
        if (Time.time >= _attackInputStartTime + _inputHoldTime)
        {
            AttackInput = false;
        }
    }

}
