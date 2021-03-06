﻿using System.Collections;
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
    public int FacingDirection { get; private set; }

    [SerializeField]
    private PlayerData _playerData;
    private Vector2 _workspace;
    #endregion

    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        FacingDirection = 1;
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
        Debug.Log("SET VELOCITY -X- is being executed");
    }

    public void SetVelocityY(float jumpVelocity)
    {
        _workspace.Set(RB.velocity.x, jumpVelocity);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
        Debug.Log("SET VELOCITY -Y- is being executed");
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
