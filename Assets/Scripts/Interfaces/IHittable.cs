using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INormalHittable 
{
    void RecieveGroundedNormalHit( int playerFacingDirection);
}

public interface IChainHittable
{
    void RecieveToAirHit(int playerFacingDirection);
    void RecievePushHit();
    void RecieveStunHit();
}

public interface ICanHandleHits
{
    void HandleGroundedNormalHit(int playerFacingDirection);
    void HandleToAirHit(int playerFacingDirection);
}
