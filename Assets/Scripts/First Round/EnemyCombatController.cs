using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class EnemyCombatController : MonoBehaviour
    {
        //Config Params
        [SerializeField] float recieveNormalHitSpeed = 5f;
        [SerializeField] float recieveKnockbackHitSpeed = 15f;
        [SerializeField] float seePlayerDistance = 4f;
        [SerializeField] float canHitPlayerDistance = 1f;
        [SerializeField] float minWaitForAttack = 1f;
        [SerializeField] float maxWaitForAttack = 2f;
        [SerializeField] float attackRadius = 0.5f;
        [SerializeField] float attackHitSpeed = 5f;
        [SerializeField] float attackSpeed = 5f;
        [SerializeField] int attackDamage = 20;
        [SerializeField] Vector2 recieveAirChainHit;

        //
        [SerializeField] bool iSeePlayer = false;
        [SerializeField] bool iCanHitPlayer = false;

        //Cached Refs
        [SerializeField] Enemy enemyScript;
        [SerializeField] Transform attackHitBoxPoint = null;

        private void Start()
        {
            enemyScript = GetComponent<Enemy>();
        }

        private void Update()
        {
            CheckForPlayer();
        }

        private void FixedUpdate()
        {
            TrackOrAttackPlayer();
        }

        void CheckForPlayer()
        {
            iSeePlayer = Physics2D.Raycast(transform.position, transform.right, seePlayerDistance, LayerMask.GetMask("Player"));
            iSeePlayer = Physics2D.Raycast(transform.position, -transform.right, seePlayerDistance, LayerMask.GetMask("Player"));
            iCanHitPlayer = Physics2D.Raycast(transform.position, transform.right, canHitPlayerDistance, LayerMask.GetMask("Player"));


        }

        void TrackOrAttackPlayer()
        {
            if (enemyScript.isDead) { return; }

            if (iSeePlayer && !iCanHitPlayer)
            {
                float playerXPos = FindObjectOfType<PlayerController>().transform.position.x;
                LookTowardsPlayer(playerXPos);
            }

            if (iCanHitPlayer)
            {
                StartCoroutine("AttackPlayer");
            }
        }

        IEnumerator AttackPlayer()
        {
            if (!enemyScript.isAttacking)
            {
                float randomWait = Random.Range(minWaitForAttack, maxWaitForAttack);
                //Debug.Log("Random Wait = " + randomWait);

                yield return new WaitForSeconds(randomWait);
                if (!enemyScript.isAttacking)
                {
                    enemyScript.isAttacking = true;
                    Debug.Log("Kurai yagare!");
                }
            }
        }

        void ApplyAttackMovement()
        {
            Vector2 forceToAdd = new Vector2(attackSpeed, 0f);
            GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Impulse);
        }

        public void CheckAttackHitbox()
        {
            Collider2D[] detecterdObjects = Physics2D.OverlapCircleAll(attackHitBoxPoint.position, attackRadius, LayerMask.GetMask("Player"));
            foreach (Collider2D collider in detecterdObjects)
            {
                if (collider.gameObject.tag == "Player")   //Redundante? -------------------------------------------------------> ^^Por esto^^
                {
                    collider.GetComponent<Health>().DecreaseHealth(attackDamage);
                    collider.GetComponent<CombatTest>().RecieveHit(attackHitSpeed);
                }
            }
        }

        public void FinishAttack()
        {
            enemyScript.isAttacking = false;
        }

        void LookTowardsPlayer(float playerXPos)
        {
            //  Calculo la diferencia entre los ejes x del personaje y del enemigo
            //  -Si la diferencia es mayor a 0 el personaje estaría a la izquierda del enemigo
            //  -Si la diferencia es menor a 0 el personaje estaría a la derecha del enemigo

            float xDifference = transform.position.x - playerXPos;
            if (Mathf.Sign(xDifference) > 0 && Mathf.Sign(enemyScript.facingDirection) == 1)
            {
                enemyScript.Flip();
            }
            else if (Mathf.Sign(xDifference) < 0 && Mathf.Sign(enemyScript.facingDirection) == -1)
            {
                enemyScript.Flip();
            }
        }


        public void RecieveHit(int damage, float playerFacingDirection, int comboTracker)
        {
            if (!enemyScript.isDead)
            {
                Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
                if (enemyScript.facingDirection == playerFacingDirection)
                {
                    enemyScript.Flip();
                }
                enemyScript.isBeingAttacked = true;
                GetComponent<Health>().DecreaseHealth(damage);

                switch (comboTracker)
                {
                    case 5:
                        Vector2 forceToAdd = new Vector2(recieveKnockbackHitSpeed * playerFacingDirection, 0f);
                        enemyRigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
                        break;
                    default:
                        Vector2 forceToAdd2 = new Vector2(recieveNormalHitSpeed * playerFacingDirection, 0f);
                        enemyRigidbody.AddForce(forceToAdd2, ForceMode2D.Impulse);
                        break;

                }
                Debug.Log("I've been hit by someone (>:c)");
            }
        }

        public void FinishRecieveHit()
        {
            enemyScript.isBeingAttacked = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            //Gizmos.DrawRay(new Ray(transform.position, new Vector3(transform.position.x + seePlayerDistance, transform.position.y + 1)));
        }



    }

}
