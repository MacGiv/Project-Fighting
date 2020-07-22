using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{


    private void OnEnable()
    {
        
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.started == true)
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
