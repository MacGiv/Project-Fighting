using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAnim : MonoBehaviour
{
    public float timeToStart = 1f;
    [Range(1, 2), Tooltip("Idle Animation = 1 || Run Animation = 2")] 
    public int whatAnim = 1;

    private Animator _anim => GetComponent<Animator>();


    void Start()
    {
        Invoke("ActivateAnimation", timeToStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void ActivateAnimation()
    {
        switch (whatAnim)
        {
            case 1:
                {
                    _anim.SetBool("checkIdle", true);
                    break;
                }
            case 2:
                {
                    _anim.SetBool("checkRun", true);
                    break;
                }
        }
    }
}
