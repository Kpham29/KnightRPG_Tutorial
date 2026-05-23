using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;

    public float shootCooldown = .5f;
    private float shootTimer;

    public Animator animator;

    public PlayerMovement playerMovement;

    private void OnEnable()
    {
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 1);
    }

    private void OnDisable()
    {
        animator.SetLayerWeight(0, 1);
        animator.SetLayerWeight(1, 0);
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            animator.SetBool("isShooting", true);
        }
    }

    public void Shoot()
    {
        if (shootTimer <= 0)
        {
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.direction = aimDirection;
            shootTimer = shootCooldown;
            animator.SetBool("isShooting", false);
            playerMovement.isShooting = false;
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("aimX", aimDirection.x);
            animator.SetFloat("aimY", aimDirection.y);
        }
    }
}