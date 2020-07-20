using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class Enemy : MonoBehaviour
    {
        //Config Parameters
        [SerializeField] float movementSpeed = 5f;
        //[SerializeField] float recieveHitSpeed = 5f;
        //[SerializeField] float recieveHeavyHitSpeed = 15f;
        [SerializeField] float wallCheckDistance = 1f;
        [SerializeField] float groundCheckDistance = 2f;
        [SerializeField] float ledgeCheckDistance = 10f;
        [SerializeField] float waitTillDestroy = 1f;

        public float facingDirection = 1f;

        //Cached References
        [SerializeField] Rigidbody2D enemyRigidbody;


        //Debug Bools
        [SerializeField] bool enemyTouchingWall;
        [SerializeField] bool isMoving;
        [SerializeField] bool isGrounded;

        public bool isDead = false;
        public bool isAttacking { get; set; }
        public bool isBeingAttacked { get; set; }

        [SerializeField] bool isTouchingLedge = true;

        void Start()
        {
            enemyRigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            UpdateAnimations();
            CheckMovementSurroundings();
        }

        private void FixedUpdate()
        {
            if (!isDead && isGrounded)
            {
                CheckWall();
                CheckLedge();
                Move();
            }
        }

        void Move()
        {
            if (!isAttacking && !isBeingAttacked)
            {
                //isMoving = true;
                enemyRigidbody.velocity = new Vector2(movementSpeed * facingDirection, enemyRigidbody.velocity.y);
            }

            if (isMoving = (Mathf.Abs(enemyRigidbody.velocity.x) > 1f))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        void CheckMovementSurroundings()
        {
            enemyTouchingWall = Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));
            isTouchingLedge = Physics2D.Raycast(transform.position,
                                                Vector2.Lerp(new Vector2(facingDirection, 0), Vector2.down, 0.5f),
                                                ledgeCheckDistance, LayerMask.GetMask("Ground"));
            isGrounded = Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, LayerMask.GetMask("Ground"));

            Debug.DrawRay(transform.position, Vector2.Lerp(new Vector2(facingDirection * ledgeCheckDistance, 0), Vector2.down, 0.5f), Color.red, 0.1f);
        }

        void CheckLedge()
        {
            if (!isTouchingLedge)
            {
                Flip();
            }
        }

        void CheckWall()
        {
            if (enemyTouchingWall)
            {
                Flip();
            }
        }

        public void Flip()
        {
            if (!isAttacking)
            {
                facingDirection = -facingDirection;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        public void FinishRecieveHit()
        {
            isBeingAttacked = false;
        }

        public void Die()
        {
            if (!isDead)
            {
                isDead = true;
            }
        }

        //DestoyMe() se ejecuta en el ultimo frame de la animación de la muerte
        public IEnumerator DestroyMe()
        {
            enemyRigidbody.bodyType = RigidbodyType2D.Static;
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;

            yield return new WaitForSeconds(waitTillDestroy);
            //Destroy(gameObject);

        }

        void UpdateAnimations()
        {
            Animator myAnimator = GetComponent<Animator>();
            if (isDead)
            {
                myAnimator.SetBool("isDead", isDead);
            }
            else
            {
                myAnimator.SetBool("isBeingAttacked", isBeingAttacked);
                myAnimator.SetBool("isMoving", isMoving);
                myAnimator.SetBool("isAttacking", isAttacking);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y, transform.position.z));
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + ledgeCheckDistance * facingDirection, transform.position.y + -ledgeCheckDistance, transform.position.z));
        }

        private void OnTriggerStay2D(Collider2D playercCollider)
        {
            if (isDead) { return; }

            if (playercCollider.gameObject.GetComponent<PlayerController>() != null)
            {
                float playerXPos = playercCollider.transform.position.x;
                LookTowardsPlayer(playerXPos);
            }
        }

        void LookTowardsPlayer(float playerXPos)
        {
            //  Calculo la diferencia entre los ejes x del personaje y del enemigo
            //  -Si la diferencia es mayor a 0 el personaje estaría a la izquierda del enemigo
            //  -Si la diferencia es menor a 0 el personaje estaría a la derecha del enemigo

            float xDifference = transform.position.x - playerXPos;
            if (Mathf.Sign(xDifference) > 0 && Mathf.Sign(facingDirection) == 1)
            {
                Flip();
            }
            else if (Mathf.Sign(xDifference) < 0 && Mathf.Sign(facingDirection) == -1)
            {
                Flip();
            }
        }
    }
}


