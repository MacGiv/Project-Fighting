using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Recieve Hit Velocity")]
    public float recievedNormHitVelocity = 200f;
    public float recieveToAirHitVelocityY = 350f;
    public float recieveToAirHitVelocityX = 300f;
    public float recievePushHitVelocity = 15f;
    public float recievePushDownHitVelocity = 20f;

    [Header("Receive Hit Timers Set")]
    public float normalHitDuration = 0.2f;
    public float pushHitDuration = 0.5f;
    public float toAirHitDuration = 0.2f;
    public float airHitDuration = 0.25f;

    [Header("Receive hit repositioning")]
    public float distanceToPlayerPos = 1f;

    [Header("Checkers")]
    public float wallCheckDistance = 0.9f;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundLayer;



}
