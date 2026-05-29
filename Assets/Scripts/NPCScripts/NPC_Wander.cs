using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander area")] public float wanderWidth = 5;
    public float wanderHeight = 5;
    public Vector2 startingPosition;
    public float pauseDuration = 1f;
    public float speed = 2f;
    public Vector2 target;
    
    private bool isPaused;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNewDestination());
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(transform.position, target) < .1f)
            StartCoroutine(PauseAndPickNewDestination());
        Move();
    }

    private void Move()
    {
        Vector2 direction = target - (Vector2)transform.position;
        if(direction.x > 0 && transform.localScale.x < 0 || direction.x < 0 && transform.localScale.x > 0)
            transform.localScale = new  Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.velocity = direction.normalized * speed;
    }

    IEnumerator PauseAndPickNewDestination()
    {
        isPaused = true;
        animator.Play("Pawn_idle");
        yield return new WaitForSeconds(pauseDuration);
        
        target = GetRandomTarget();
        isPaused = false;
        animator.Play("Pawn_walk");
    }

    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2;
        float halfHeight = wanderHeight / 2;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth,
                Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)), //left
            1 => new Vector2(startingPosition.x + halfWidth,
                Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)), // right
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth),
                startingPosition.y - halfHeight), // bottom
            _ => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth),
                startingPosition.y + halfHeight), // top
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(PauseAndPickNewDestination());
    }
}
