using System;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1;
    private Transform player;

    private Rigidbody2D rb;
    private EnemyState state;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    private void Update()
    {
        if (state != EnemyState.Knockback)
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }

            if (state == EnemyState.Chasing) Chase();
            else if (state == EnemyState.Attacking)
            {
                // Do attack stuff
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            // if the player is in attack range AND cooldown is ready
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange &&
                attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange &&
                     state != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    private void Chase()
    {
        if ((player.position.x > transform.position.x && facingDirection == -1) ||
            (player.position.x < transform.position.x && facingDirection == 1))
            Flip();
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeState(EnemyState newState)
    {
        if (state == EnemyState.Idle)
            animator.SetBool("isIdle", false);
        else if (state == EnemyState.Chasing)
            animator.SetBool("isChasing", false);
        else if (state == EnemyState.Attacking)
            animator.SetBool("isAttacking", false);

        state = newState;

        if (state == EnemyState.Idle)
            animator.SetBool("isIdle", true);
        else if (state == EnemyState.Chasing)
            animator.SetBool("isChasing", true);
        else if (state == EnemyState.Attacking)
            animator.SetBool("isAttacking", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback
}