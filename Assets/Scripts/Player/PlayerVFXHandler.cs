using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXHandler : MonoBehaviour
{
    public GameObject normalHitParticles;
    public GameObject normalHitParticles_Down;

    public Transform hitVFXPosition;


    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    public void PlayNormalHitVFX()
    {
        Instantiate(normalHitParticles, hitVFXPosition);
    }

    public void PlayDownHitVFX()
    {
        Instantiate(normalHitParticles_Down, hitVFXPosition);
    }
}
