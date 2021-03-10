using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Cached Components

    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection = 1;

    [SerializeField]
    private PlayerData _playerData;
    private Vector2 _workspace;
    #endregion

    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        //FacingDirection = 1;  FacingDirection was a property 
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
    }

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, RB.velocity.y);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityY(float jumpVelocity)
    {
        _workspace.Set(RB.velocity.x, jumpVelocity);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetWallJumpVelocity(float jumpVelocityX, float jumpVelocityY)
    {
        _workspace.Set(jumpVelocityX * -FacingDirection, jumpVelocityY);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetDoubleDirectionalVelocity(float velocityX, float velocityY)
    {
        _workspace.Set(velocityX * FacingDirection, velocityY);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void ClimbLedge()
    {
        Vector2 oldPos = transform.position;
        Vector2 newPos = new Vector2(oldPos.x + _playerData.addToXPosition * FacingDirection, oldPos.y + _playerData.addToYPosition);
        transform.position = newPos;
    }

    public void StopAllMovement()
    {
        _workspace.Set(0f, 0f);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

}
