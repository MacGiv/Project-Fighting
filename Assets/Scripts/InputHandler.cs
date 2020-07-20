using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Debug.Log("MOVEMENT input pressed!" + context);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP input button pressed!");
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        Debug.Log("DASH input button pressed!");
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        Debug.Log("ATTACK input button pressed!");
    }

}
