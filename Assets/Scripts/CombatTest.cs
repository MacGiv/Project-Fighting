using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class CombatTest : MonoBehaviour
    {
        [Header("Configuration Parameters")]
        [SerializeField] float hitSpeed = 0;
        [SerializeField] [Range(0f, 4f)] float gravityWhileHitting = 0;

        [Tooltip("How long to hold that an input that was pressed")]
        [SerializeField] float inputTimer = 0; //How long to hold that an input that was pressed
        [SerializeField] float attack1Radius = 0;
        [SerializeField] float comboLostTime = 0;
        [SerializeField] int attackDamage = 0;
        [SerializeField] int comboTracker = 1;

        [Header("Debug Booleans")]

        [SerializeField] bool gotInput;
        [SerializeField] public bool combatEnabled { get; set; }
        [SerializeField] public bool isAttacking { get; set; }
        [SerializeField] public bool isBeingAttacked { get; set; }

        [Header("Cached References")]
        [SerializeField] Transform attackHitBoxPos = null;
        [SerializeField] LayerMask whatIsDamageable;
        Animator playerAnimator;

        float lastInputTime = -100; //Stores the last time we attempted to attack 
        float lastAttackTime = -1;
        float normalGravityScale;

        private void Awake()
        {
            normalGravityScale = GetComponent<Rigidbody2D>().gravityScale;
            combatEnabled = true;
            playerAnimator = GetComponent<Animator>();
            playerAnimator.SetBool("canAttack", combatEnabled);
            comboTracker = 1;
        }

        private void Update()
        {
            CheckCombatInput();
            CheckAttacks();
        }

        void CheckCombatInput()
        {
            if (Input.GetButtonDown("Attack1"))
            {
                if (combatEnabled)
                {
                    //Attempt combat (Hold the input so if we press a little bit before we are able to hit the character will still hit once he is able -like the jump-)
                    gotInput = true;
                    lastInputTime = Time.time;
                }
            }
        }

        void CheckAttacks() //Makes the attack happen when we got an input
        {
            if (gotInput)
            {
                PlayerController playerController = GetComponent<PlayerController>();
                bool isDashing = playerController.isDashing;
                //Perform attack 1
                if (!isAttacking && !isDashing && GetComponent<Rigidbody2D>().velocity.y >= -0.5f) //If you are not in a attack animation ----- Agregado: and not dashing either
                {
                    if (comboTracker == 4)
                    {
                        FindObjectOfType<GameSpeed>().SetSlowAttackSpeed();
                    }
                    playerController.StopMovement();
                    SetAttackGravity();
                    ApplyAttackMovement(GetComponent<Rigidbody2D>(), playerController.facingDirection);
                    gotInput = false;
                    isAttacking = true;
                    playerAnimator.SetBool("isAttacking", isAttacking);
                    playerAnimator.SetInteger("comboTracker", comboTracker);
                    lastAttackTime = Time.time;
                    comboTracker++;
                }
            }

            if (Time.time >= lastAttackTime + comboLostTime)
            {
                comboTracker = 1;
            }

            if (Time.time >= lastInputTime + inputTimer)
            {
                gotInput = false;
                //Wait for a new input
            }
        }

        void CheckAttackHitBox()
        {
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attack1Radius, whatIsDamageable);
            FindObjectOfType<GameSpeed>().SetNormalSpeed(); //Set normal game speed after fourth hit
            foreach (Collider2D enemyCollider in detectedObjects)
            {
                enemyCollider.GetComponent<EnemyCombatController>().RecieveHit(attackDamage, GetComponent<PlayerController>().facingDirection, comboTracker);

                //Instantiate hit particle
            }
        }

        void FinishAttack1()
        {
            isAttacking = false;
            if (comboTracker > 4)
            {
                comboTracker = 1;
            }
            playerAnimator.SetBool("isAttacking", isAttacking);
            playerAnimator.SetInteger("comboTracker", comboTracker);
            SetNormalGravity();
        }

        void ApplyAttackMovement(Rigidbody2D playerRigidbody, int facingDirection)
        {
            Vector2 forceToAdd = new Vector2(hitSpeed * facingDirection, 0f);
            playerRigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
        }

        public void RecieveHit(float recievedHitSpeed)
        {
            if (!isBeingAttacked)
            {
                isBeingAttacked = true;
                GetComponent<PlayerController>().DisableFlip();
                GetComponent<Animator>().SetBool("hasRecievedHit", true);
                SetAttackGravity();
                ApplyRecieveHitMovement(recievedHitSpeed);
            }
        }

        void ApplyRecieveHitMovement(float recievedHitSpeed)
        {
            Vector2 forceToAdd = new Vector2(recievedHitSpeed, 0f);
            GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);

        }

        public void FinishRecieveHit()
        {
            GetComponent<PlayerController>().EnableFlip();
            GetComponent<Animator>().SetBool("hasRecievedHit", false);
            SetNormalGravity();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackHitBoxPos.position, attack1Radius);
        }

        void SetAttackGravity()
        {
            GetComponent<Rigidbody2D>().gravityScale = gravityWhileHitting;
        }

        void SetNormalGravity()
        {
            GetComponent<Rigidbody2D>().gravityScale = normalGravityScale;
        }

    }
}


