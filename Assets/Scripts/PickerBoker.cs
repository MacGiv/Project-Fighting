using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerBoker : MonoBehaviour
{
    //---------------------------------------------------
    //---------------------------------------------------
    //-----------|| Back Up de CombatTest ||-------------
    //---------------------------------------------------
    //---------------------------------------------------

    /*
    [Header("Configuration Parameters")]
    [SerializeField] float hitSpeed;
    [SerializeField] [Range(0f, 4f)] float gravityWhileHitting;

    [Tooltip("How long to hold that an input that was pressed")]
    [SerializeField] float inputTimer; //How long to hold that an input that was pressed
    [SerializeField] float attack1Radius;
    [SerializeField] int attackDamage;

    [Header("Debug Booleans")]

    [SerializeField] bool gotInput;
    [SerializeField] public bool combatEnabled { get; set; }
    [SerializeField] public bool isAttacking { get; set; }
    [SerializeField] bool isFirstAttack;

    [Header("Cached References")]
    [SerializeField] Transform attack1HitBoxPos;
    [SerializeField] LayerMask whatIsDamageable;
    Animator playerAnimator;

    float lastInputTime = -100; //Stores the last time we attempted to attack 
    float normalGravityScale;

    private void Awake()
    {
        normalGravityScale = GetComponent<Rigidbody2D>().gravityScale;
        combatEnabled = true;
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("canAttack", combatEnabled);
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
            if (!isAttacking && !isDashing) //If you are not in a attack animation ----- Agregado: and not dashing either
            {
                playerController.StopMovement();
                SetAttackGravity();
                ApplyAttackMovement(GetComponent<Rigidbody2D>(), playerController.facingDirection);
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                playerAnimator.SetBool("attack1", true);
                playerAnimator.SetBool("firstAttack", isFirstAttack);
                playerAnimator.SetBool("isAttacking", isAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
            //Wait for a new input
        }
    }

    void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);
        foreach (Collider2D collider in detectedObjects)
        {
            collider.GetComponent<Enemy>().RecieveHit(attackDamage, GetComponent<PlayerController>().facingDirection);

            //collider.transform.SendMessage("RecieveHit", attack1Damage, GetComponent<PlayerController>().facingDirection);
            //Instantiate hit particle
        }
    }

    void FinishAttack1()
    {
        isAttacking = false;
        playerAnimator.SetBool("isAttacking", isAttacking);
        playerAnimator.SetBool("attack1", false);
        SetNormalGravity();
    }

    void ApplyAttackMovement(Rigidbody2D playerRigidbody, int facingDirection)
    {
        Vector2 forceToAdd = new Vector2(hitSpeed * facingDirection, 0f);
        playerRigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    void SetAttackGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = gravityWhileHitting;
    }

    void SetNormalGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = normalGravityScale;
    }
    */
}
