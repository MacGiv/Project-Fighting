using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImNormalHittable 
{
    void RecieveNormalHit( int playerFacingDirection);
}

public interface ImSpecialHittable
{
    void ReceiveToAirHit(int playerFacingDirection);
    void ReceivePushHit(int playerFacingDirection);
    void ReceiveStunHit();
}


public interface ICanHandleNormalHits
{
    void HandleGroundedNormalHit(int playerFacingDirection);
}

public interface ICanHandleSpecialHits
{
    void HandleToAirHit(int playerFacingDirection);
    void HandlePushHit(int playerFacingDirection);
    void HandleStunHit(int playerFacingDirection);
}
