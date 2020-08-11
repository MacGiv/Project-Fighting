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

    [SerializeField]
    private float _inputHoldTime = 0.2f;
    private float _jumInputStartTime;


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
        RawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            _jumInputStartTime = Time.time;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        Debug.Log("DASH input button pressed!");
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        Debug.Log("ATTACK input button pressed!");
    }


    public void JumpInputWasUsed() => JumpInput = false;

    private void CheckInputHoldTime()
    {
        if (Time.time >= _jumInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }

}
