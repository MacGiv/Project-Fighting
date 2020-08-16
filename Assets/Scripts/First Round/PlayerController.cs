using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFighting.FirstRound
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Configuration Parameters")]
        [SerializeField] float movementSpeed = 10f;
        [SerializeField] float jumpForce = 16f;
        [SerializeField] float wallJumpForce = 0;
        [SerializeField] float airDragMultiplier = 0.95f;
        [SerializeField] float wallSlideSpeed = 0;
        [SerializeField] float groundCheckRadius = 0;
        [SerializeField] float wallCheckDistance = 0;
        [Tooltip("Force in Y added when the Jump button is not being held")]
        [SerializeField] float variableJumpHeightMultiplier = 0.5f;
        [SerializeField] float dashTime = 0;
        [SerializeField] float dashSpeed = 0;
        [SerializeField] float distanceBetweenAfterimages = 0;
        [SerializeField] float dashCooldown = 0;
        [SerializeField] int amountOfJumps = 1;
        [SerializeField] Vector2 wallJumpDirection;
        int amountOfJumpsLeft = 1;

        //Timers
        float jumpTimer;
        [SerializeField] float turnTimer;
        float wallJumpTimer;
        float dashTimeLeft;
        float lastAfterimageXPos;
        float lastDash = -100f;

        [Tooltip("Place in the X axis where the Player GameObject is when it starts climbing the ledge")]
        [SerializeField] float ledgeClimbXOffset1 = 0f;
        [Tooltip("Place in the Y axis where the Player GameObject is when it starts climbing the ledge")]
        [SerializeField] float ledgeClimbYOffset1 = 0f;
        [Tooltip("Place in the X axis where the Player GameObject is when it finishes climbing the ledge")]
        [SerializeField] float ledgeClimbXOffset2 = 0f;
        [Tooltip("Place in the Y axis where the Player GameObject is when it finishes climbing the ledge")]
        [SerializeField] float ledgeClimbYOffset2 = 0f;

        [Header("Timers Setters")]
        [SerializeField] float jumpTimerSet = 0.15f;
        [SerializeField] float turnTimerSet = 0.1f;
        [SerializeField] float wallJumpTimerSet = 0.5f;

        [Header("Cached References")]
        [SerializeField] Transform groundCheck = null;
        [SerializeField] Transform wallCheck = null;
        [SerializeField] Transform ledgeCheck = null;
        [SerializeField] LayerMask whatIsGround;

        [Header("Debug Booleans")]
        [SerializeField] private bool isWalking = false;
        [SerializeField] private bool isFacingRight = true;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool canNormalJump;
        [SerializeField] private bool canWallJump;
        [SerializeField] private bool isTouchingWall;
        [SerializeField] private bool isWallSliding;
        [SerializeField] private bool isAttemptingToJump;
        [SerializeField] public bool canMove;
        [SerializeField] public bool canFlip;
        [SerializeField] public bool isDashing;

        [Header("Debug Ints")]
        [SerializeField] public int facingDirection = 1;
        [SerializeField] int lastWallJumpDirection;

        //checkJumpMultiplier Checks if the player has stopped pushing the jump button
        private bool checkJumpMultiplier;
        private bool hasWallJumped;
        private bool isTouchingLedge;
        private bool canClimbLedge = false;
        private bool ledgeDetected;
        private float movementInputDirection;

        private Rigidbody2D rb;
        private Animator characterAnimator;
        private Vector2 ledgePosBot;
        private Vector2 ledgePos1;
        private Vector2 ledgePos2;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            characterAnimator = GetComponent<Animator>();
            amountOfJumpsLeft = amountOfJumps;
            wallJumpDirection.Normalize();
        }

        void Update()
        {
            CheckInput();
            CheckMovementDirection();
            CheckIfWallSliding();
            CheckIfCanJump();
            CheckJump();
            CheckLedgeClimb();
            CheckDash();
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            CheckSurroundings();
        }

        private void CheckSurroundings()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
            isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

            if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
            {
                ledgeDetected = true;
                ledgePosBot = wallCheck.position;
            }
        }

        private void CheckInput()
        {
            movementInputDirection = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
                {
                    NormalJump();
                }
                else
                {
                    jumpTimer = jumpTimerSet;
                    isAttemptingToJump = true;
                }

            }

            if (Input.GetButtonDown("Horizontal") && isTouchingWall)
            {
                if (!isGrounded && movementInputDirection != facingDirection)
                {
                    canMove = false;
                    canFlip = false;

                    turnTimer = turnTimerSet;
                }
            }
            if (turnTimer >= 0)
            {
                turnTimer -= Time.deltaTime;
                if (turnTimer <= 0)
                {
                    canMove = true;
                    canFlip = true;
                }
            }

            if (checkJumpMultiplier && !Input.GetButton("Jump"))
            {
                checkJumpMultiplier = false;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
            }

            if (Input.GetButtonDown("Dash"))
            {
                if (Time.time >= (lastDash + dashCooldown))
                {
                    AttemptToDash();
                }
            }

        }

        //
        //
        ////////////   DASH    /////////////
        //
        //

        void AttemptToDash()
        {
            bool isAttacking = GetComponent<CombatTest>().isAttacking;
            if (!isAttacking)
            {
                isDashing = true;
                dashTimeLeft = dashTime;
                lastDash = Time.time;

                PlayerAfterImagePool.Instance.GetFromPool();
                lastAfterimageXPos = transform.position.x;
            }
        }

        void CheckDash()
        {
            if (isDashing)
            {
                if (dashTimeLeft > 0)
                {
                    canMove = false;
                    canFlip = false;
                    rb.velocity = new Vector2(dashSpeed * facingDirection, 0f);
                    dashTimeLeft -= Time.deltaTime;


                    //Now checks if enough time has passed to place another afterimage
                    if (Mathf.Abs(transform.position.x - lastAfterimageXPos) > distanceBetweenAfterimages)
                    {
                        PlayerAfterImagePool.Instance.GetFromPool();
                        lastAfterimageXPos = transform.position.x;
                    }
                }

                if (dashTimeLeft <= 0 || isTouchingWall)
                {
                    isDashing = false;
                    canMove = true;
                    canFlip = true;
                }

            }
        }

        //
        //
        ////////////  LEDGE CLIMB   /////////////
        //
        //

        private void CheckLedgeClimb()
        {
            if (ledgeDetected && !canClimbLedge)
            {
                canClimbLedge = true;

                if (isFacingRight)
                {
                    ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                    ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
                }
                else
                {
                    ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                    ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
                }

                canMove = false;
                canFlip = false;
            }

            if (canClimbLedge)
            {
                transform.position = ledgePos1;
            }

            characterAnimator.SetBool("canClimbLedge", canClimbLedge);
        }

        public void FinishLedgeClimb()
        {
            canClimbLedge = false;
            transform.position = ledgePos2;
            canMove = true;
            canFlip = true;
            ledgeDetected = false;
            characterAnimator.SetBool("canClimbLedge", canClimbLedge);
        }

        //
        //
        ////////////   MOVEMENT    /////////////
        //
        //

        private void CheckMovementDirection()
        {
            if (isFacingRight && movementInputDirection < 0 && !isWallSliding)
            {
                Flip();
            }
            else if (!isFacingRight && movementInputDirection > 0 && !isWallSliding)
            {
                Flip();
            }

            if (rb.velocity.x >= 0.3 || rb.velocity.x <= -0.3) //Si hay problemas reemplazar por MAthf.Abs(rb.velocity.x) > 0.01 ó 0.3
                                                               //                                                         
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }


        }

        private void ApplyMovement()
        {
            if (GetComponent<CombatTest>().isAttacking || isDashing)
            {
                return;
            }

            if (!isGrounded && !isWallSliding && movementInputDirection == 0) //&& !isDashing AGREGADO
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }
            else if (canMove)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }

            if (isWallSliding)
            {
                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.velocity = new Vector2(0f, -wallSlideSpeed);
                }
            }
        }

        //
        //
        ////////////   WALL SLIDE    /////////////
        //
        //

        private void CheckIfWallSliding()
        {
            /*if (!isGrounded && isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0 && !canClimbLedge)
            {
                isWallSliding = true;
                canFlip = false;
                canMove = false;
            }
            else*/
            if (!isGrounded && isTouchingWall && rb.velocity.y < 0 && !canClimbLedge) //agregado 
            {
                isWallSliding = true;
                canFlip = false;
                canMove = false;
            }
            else
            {
                isWallSliding = false;
                if (!isWallSliding && !GetComponent<CombatTest>().isAttacking)
                {
                    canFlip = true;
                    canMove = true;
                }
            }
        }

        //
        //
        ////////////   JUMP    /////////////
        //
        //

        private void CheckIfCanJump()
        {
            if (GetComponent<CombatTest>().isAttacking == true)
            {
                canNormalJump = false;
                canWallJump = false;
                return;
            }

            if (isGrounded && rb.velocity.y <= 0.01f) //Mathf.Abs(rb.velocity.y)
            {
                amountOfJumpsLeft = amountOfJumps;
            }

            if (isWallSliding) //isTouchingWall
            {
                canWallJump = true;
            }

            if (!isGrounded) // amountOfJumpsLeft <= 0 &&
            {
                canNormalJump = false;
            }
            else
            {
                canNormalJump = true;
            }
        }

        private void CheckJump()
        {
            if (jumpTimer > 0)
            {
                if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
                {
                    WallJump();
                    Debug.Log("i can wall jump");
                }
                else if (isGrounded)
                {
                    NormalJump();
                }
            }

            if (isAttemptingToJump)
            {
                jumpTimer -= Time.deltaTime;
            }
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer >= 0)
            {
                wallJumpTimer -= Time.deltaTime;
            }

        }

        private void NormalJump()
        {
            if (canNormalJump && !isWallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                amountOfJumpsLeft--;
                jumpTimer = 0;
                isAttemptingToJump = false;
                checkJumpMultiplier = true;
            }
        }

        private void WallJump()
        {
            if (canWallJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                //amountOfJumpsLeft = amountOfJumps;
                amountOfJumpsLeft--;
                Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
                rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                isWallSliding = false;
                jumpTimer = 0;
                isAttemptingToJump = false;
                checkJumpMultiplier = true;
                turnTimer = 0;
                canMove = true;
                canFlip = true;
                hasWallJumped = true;
                wallJumpTimer = wallJumpTimerSet;
                lastWallJumpDirection = -facingDirection;
            }
        }

        //
        //
        ////////////   FLIP & MISC    /////////////
        //
        //

        private void Flip()
        {
            if (!isWallSliding && canFlip)
            {
                facingDirection *= -1;
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        public void DisableFlip()
        {
            canFlip = false;
            canMove = false;
            //Debug.Log("I can't flip");
        }

        public void EnableFlip()
        {
            canFlip = true;
            canMove = true;
            //Debug.Log("I can flip now");
        }


        public void StopMovement()
        {
            rb.velocity = Vector2.zero;
        }

        private void UpdateAnimations()
        {
            characterAnimator.SetBool("isWallSliding", isWallSliding);
            characterAnimator.SetBool("isWalking", isWalking);
            characterAnimator.SetBool("isGrounded", isGrounded);
            characterAnimator.SetBool("isDashing", isDashing);
            characterAnimator.SetFloat("yVelocity", rb.velocity.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

            Gizmos.DrawLine(wallCheck.position,
                new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }



    }

}
