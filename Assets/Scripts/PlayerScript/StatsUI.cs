using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlot;
    public CanvasGroup statsCanvas;
    private bool statsOpen = false;
    
    private void Start()
    {
        statsCanvas.blocksRaycasts = false;
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
        }
    }

    public void UpdateDamage()
    {
        statsSlot[0].GetComponentInChildren<TMP_Text>().text = "Damage: "+StatManager.Instance.damage.ToString();
    }
    
    public void UpdateSpeed()
    {
        statsSlot[1].GetComponentInChildren<TMP_Text>().text = "Speed: "+StatManager.Instance.speed.ToString();
    }

    public void UpdateHealth()
    {
        statsSlot[2].GetComponentInChildren<TMP_Text>().text = "Health: "+StatManager.Instance.maxHealth.ToString();
    }
    
    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}
