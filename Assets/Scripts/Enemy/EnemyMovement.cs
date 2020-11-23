using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _RB;
    private EnemyBrain _enemyBrain;
    private Player _playerBrain;
    public EnemyData _enemyData;

    private Vector2 _vectorWorkspace;
    public int FacingDirection = 1;

    void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _enemyBrain = GetComponent<EnemyBrain>();
        _enemyData = _enemyBrain.enemyData;
        _playerBrain = FindObjectOfType<Player>();
    }


    void Update()
    {
        
    }

    public void SetRecieveNormalHitVelocity(int playerFacingMultiplier)
    {
        Vector2 forceToAdd = new Vector2(_enemyData.recievedNormHitVelocity * playerFacingMultiplier, 0f);
        _RB.velocity = forceToAdd;
        CheckHitFlip(playerFacingMultiplier);
    }

    public void SetRecieveToAirHitVelocity(int playerFacingMultiplier)
    {
        Vector2 forceToAdd = new Vector2(_enemyData.recieveToAirHitVelocityX * playerFacingMultiplier, _enemyData.recieveToAirHitVelocityY);
        _RB.velocity = forceToAdd;
        CheckHitFlip(playerFacingMultiplier);
    }

    public void SetRecievePushHitVelocity(int playerFacingMultiplier)
    {
        Vector2 forceToAdd = new Vector2(_enemyData.recievePushHitVelocity * playerFacingMultiplier, 0f);
        _RB.velocity = forceToAdd;
        CheckHitFlip(playerFacingMultiplier);
    }

    public void SetReceivePushDownHitVelocity()
    {
        Vector2 forceToAdd = new Vector2(0f, _enemyData.recievePushDownHitVelocity);
    }

    public void SetDoubleDirectionalVelocity(int xDirection, float xForce, float yForce)
    {
        Vector2 forceToAdd = new Vector2(xForce * xDirection, yForce);
        _RB.velocity = forceToAdd;
        CheckHitFlip(xDirection);
    }

    public void StickToThePlayer(float playerFacingDirection)
    {
        Vector2 playerPos = _playerBrain.gameObject.transform.position;

        _vectorWorkspace.Set( playerPos.x + 1f * playerFacingDirection , playerPos.y);
        transform.position = _vectorWorkspace;
        CheckHitFlip((int)playerFacingDirection);

    }

    public void StickToThePlayerOnX(float playerfacingDirection)
    {
        Vector2 playerPos = _playerBrain.gameObject.transform.position;

        _vectorWorkspace.Set(playerPos.x + 1f * playerfacingDirection, _RB.position.y);
        transform.position = _vectorWorkspace;
        CheckHitFlip((int)playerfacingDirection);
    }

    public void StopAllMovement()
    {
        _RB.velocity = Vector2.zero;
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CheckHitFlip(int playerFacingMultiplier)
    {
        if (playerFacingMultiplier == FacingDirection)
        {
            Flip();
        }
    }
}
