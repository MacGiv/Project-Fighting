using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class GameSpeed : MonoBehaviour
    {
        [SerializeField] float timeOnAttack = 0.3f;
        public float gameSpedValue = 1f;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Time.timeScale = gameSpedValue;
        }

        public void SetNormalSpeed()
        {
            
        }

        public void SetSlowAttackSpeed()
        {
            Time.timeScale = timeOnAttack;
        }

    }
}


