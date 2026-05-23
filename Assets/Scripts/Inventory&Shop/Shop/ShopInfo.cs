using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Stat Fields")] public TMP_Text[] statTexts;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO item)
    {
        infoPanel.alpha = 1;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDescription;

        List<string> stats = new List<string>();
        if (item.currentHealth > 0)
        {
            stats.Add("Health: " + item.currentHealth.ToString());
        }

        if (item.maxHealth > 0)
            stats.Add("Max Health: " + item.currentHealth.ToString());
        if (item.damage > 0)
            stats.Add("Damage: " + item.damage.ToString());
        if (item.speed > 0)
            stats.Add("Speed: " + item.speed.ToString());
        if (item.duration > 0)
            stats.Add("Duration: " + item.duration.ToString());

        if (stats.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;

        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);

        infoPanelRect.position = mousePos + offset;
    }
}