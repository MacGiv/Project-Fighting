using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destroyDelay;

    void Start()
    {
        Invoke("DestroyMe", destroyDelay);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
