using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _RB;
    private EnemyBrain _enemyBrain;
    public EnemyData _enemyData;


    void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _enemyBrain = GetComponent<EnemyBrain>();
        _enemyData = _enemyBrain._enemyData;
    }


    void Update()
    {
        
    }

    public void SetRecieveNormalHitVelocity(int playerFacingMultiplier)
    {
        Vector2 forceToAdd = new Vector2(_enemyData.recievedNormHitVelocity * playerFacingMultiplier, 0f);
        _RB.AddForce(forceToAdd, ForceMode2D.Impulse);
    }

    public void SetRecieveToAirHitVelocity(int playerFacingMultiplier)
    {
        Vector2 forceToAdd = new Vector2(_enemyData.recieveToAirHitVelocityX * playerFacingMultiplier, _enemyData.recieveToAirHitVelocityY);
        _RB.AddForce(forceToAdd, ForceMode2D.Impulse);
    }

    public void SetRecievePushHitVelocity(int playerFacingMultiplier)
    {
        Vector2 forceToAdd = new Vector2(_enemyData.recievePushHitVelocity, 0f);
        _RB.AddForce(forceToAdd, ForceMode2D.Impulse);
    }
}
