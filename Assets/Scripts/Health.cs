using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class Health : MonoBehaviour
    {

        [SerializeField] int healthAtStart = 0;
        [SerializeField] public int lifePoints { get; set; }


        void Start()
        {
            lifePoints = healthAtStart;
        }

        void Update()
        {
            CheckHealth();
        }

        void Die()
        {
            Animator myAnimator = GetComponent<Animator>();
            myAnimator.SetBool("isDead", true);

        }

        void CheckHealth()
        {
            if (lifePoints <= 0)
            {
                GetComponent<Enemy>().SendMessage("Die");
            }
        }

        public void DecreaseHealth(int amount)
        {
            lifePoints -= amount;
            CheckHealth();
        }
    }

}
