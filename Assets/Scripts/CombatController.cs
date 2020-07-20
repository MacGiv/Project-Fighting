using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectFighting.FirstRound
{
    public class CombatController : MonoBehaviour
    {
        [Header("Configuration Parameters")]
        [SerializeField] float attackMoveSpeed = 0;
        [SerializeField] float comboLostTime = 0;
        [SerializeField] float continueComboTime = 0;
        [SerializeField] float hitSpotOffset = 0;
        [SerializeField] float xHitOffset = 0;
        [SerializeField] float hitRadius = 0;

        [Header("Cached References")]
        Rigidbody2D playerRb;
        PlayerController playerControllerScript;
        [SerializeField] Animation[] comboAnimationsArray;

        Collider2D[] collidersDetected;

        [SerializeField] int comboTrack = 0;

        float comboTimer;
        float lastAttackTime;

        [Header("Debug Booleans")]
        [SerializeField] bool isPerformingAttack = false;
        [SerializeField] bool gotInput = false;

        void Start()
        {
            playerRb = GetComponent<Rigidbody2D>();
            playerControllerScript = GetComponent<PlayerController>();
        }


        void Update()
        {
            CheckAttackInput();
            CheckAttackAnimation();
            CheckComboLost();
        }

        void CheckComboLost()
        {
            if (Time.time > lastAttackTime + comboLostTime)
            {
                comboTrack = 0;
            }
        }

        void CheckAttackInput()
        {
            if (Input.GetButtonDown("Attack1"))
            {
                AttemptToAttack();
            }
        }

        void AttemptToAttack()
        {
            if (isPerformingAttack)
            {
                gotInput = true;
                return;
            }
            else
            {
                HitEnemy();
            }
        }

        void CheckAttackAnimation()
        {
            Animator playerAnimator = GetComponent<Animator>();
            playerAnimator.SetFloat("comboNumber", comboTrack);
            playerAnimator.SetBool("isPerformingAttack", isPerformingAttack);
        }

        void HitEnemy()
        {
            comboTrack++;
            playerRb.AddForce(new Vector2(attackMoveSpeed, 0f));
            collidersDetected = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + xHitOffset, transform.position.y), hitRadius);
            Debug.Log(collidersDetected);
            //hacer que los que son Enemys detecten el golpe
            isPerformingAttack = true;
        }

        void AttackFinished()
        {
            isPerformingAttack = false;
            lastAttackTime = Time.time;
            playerRb.velocity = Vector2.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(new Vector3(transform.position.x + xHitOffset, transform.position.y, transform.position.z), hitRadius);
        }
    }

}

