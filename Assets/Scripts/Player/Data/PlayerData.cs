using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = -3f;
    public float downRaycastHeight = 1f;

    [Header("Wall Jump State")]
    public float wallJumpVelocityX = 15f;
    public float wallJumpVelocityY = 15f;

    [Header("Dash State")]
    public float dashDuration = 0.5f;
    public float dashVelocity = 25f;
    public float dashCooldown = 1.5f;

    [Header("Grounded Attack State")]
    public float attackVelocity = 7.5f;
    public int attackDamage = 20;
    public float comboLostTime = 0.3f;

    [Header("Air Attack State")]
    public float airAttackvelocityX = 3f;

    [Header("Combo Jump State")]
    public float comboJumpVelocityX = 15f;
    public float comboJumpVelocityY = 15f;
    public float enemyCheckDelay = 1f;

    [Header("Combo Settings")]
    public int aInputComboType = 1;
    public int bInputComboType = 2;
    public int aInputChainType = 0; // 1 = ToAir Chain | 2 = Push Chain | 3 = Stun Chain
    public int bInputChainType = 0; // 1 = ToAir Chain | 2 = Push Chain | 3 = Stun Chain
    public float chainLostTime = 0.2f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public float hitCkeckRadius = 0.3f;
    public float enemyInAirRangeDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask enemyLayer;



}
