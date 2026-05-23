using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    
    public Animator animator;
    public float cooldown = 2;
    private float timer;
    
    public StatsUI statsUI;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            animator.SetBool("isAttacking", true);

            
            timer = cooldown;
        }
        
    }

    public void DealDamage()
    {
        StatManager.Instance.damage += 1;
        statsUI.UpdateDamage();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatManager.Instance.weaponRange, enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_health>().TakeDamage(StatManager.Instance.damage);
            enemies[0].GetComponent<Enemy_knockback>().Knockback(transform, StatManager.Instance.knockbackForce, StatManager.Instance.knockbackTime, StatManager.Instance.stunTime);
        }
    }

    public void FinishAttacking()
    {
        animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatManager.Instance.weaponRange);
    }
}
