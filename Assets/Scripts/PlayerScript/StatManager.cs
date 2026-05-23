using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;
    public StatsUI statsUI;
    public TMP_Text healthText;
    
    [Header("Combat stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    
    [Header("Movement stats")] 
    public int speed;
    
    [Header("Health stats")] 
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;
    }
    
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth >= maxHealth)
            currentHealth = maxHealth;
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;
    }
    
    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateAllStats();
    }
}
