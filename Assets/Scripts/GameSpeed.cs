using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class GameSpeed : MonoBehaviour
    {
        [SerializeField] float timeOnAttack = 0.3f;
        [SerializeField] float normalTimeScale = 1f;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetNormalSpeed()
        {
            Time.timeScale = normalTimeScale;
        }

        public void SetSlowAttackSpeed()
        {
            Time.timeScale = timeOnAttack;
        }

    }
}


