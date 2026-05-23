using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int facinngDirection = 1;
    public Rigidbody2D rb;
    public Animator animator;

    private bool isKnockedBack;
    public bool isShooting;

    public PlayerCombat playerCombat;

    private void Update()
    {
        if (Input.GetButtonDown("Slash") && playerCombat.enabled == true)
        {
            playerCombat.Attack();
        }
    }

    // Update is called 0.02 frame
    private void FixedUpdate()
    {
        if (isShooting == true)
        {
            rb.velocity = Vector2.zero;
        }
        else if (isKnockedBack == false)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
                Flip();

            animator.SetFloat("speed", new Vector2(horizontal, vertical).normalized.magnitude);

            rb.velocity = new Vector2(horizontal, vertical).normalized * StatManager.Instance.speed;
        }
    }

    private void Flip()
    {
        facinngDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void KnockBack(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockBackCounter(stunTime));
    }

    IEnumerator KnockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}