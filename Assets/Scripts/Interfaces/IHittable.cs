using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INormalHittable 
{
    void RecieveNormalHit( int playerFacingDirection);
}

public interface IAirHittable
{
    void ReceiveAirHit();
}

public interface IFinishable
{
    void ReceivePKCFinisher();
    void ReceiveKOCFinisher();
    void ReceiveAACFinisher();
}


public interface ICanHandleNormalHits
{
    void HandleGroundedNormalHit(int playerFacingDirection);
}

public interface ICanHandleAirHits
{
    void HandleAirHit(int playerFacingDirection);
}

public interface ICanHandleFinishers
{
    void HandlePKCFinisher(int playerFacingDirection);
    void HandleKOCFinisher();
    void HandleAACFinisher();
}

public interface ICanHandleSpecialHits
{
    void HandleToAirHit(int playerFacingDirection);
    void HandlePushHit(int playerFacingDirection);
    void HandleStunHit(int playerFacingDirection);
    void HandlePushDownHit();
}
