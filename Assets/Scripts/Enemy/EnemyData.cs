using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Recieve Hit Velocities")]
    public float recievedNormHitVelocity = 3f;
    public float recieveToAirHitVelocityY = 10f;
    public float recieveToAirHitVelocityX = 7f;
    public float recievePushHitVelocity = 15f;
    




}
