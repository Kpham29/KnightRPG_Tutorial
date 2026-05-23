using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public UseItem useItem;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPrefab;
    public Transform player;

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    private void Start()
    {
        foreach (var slot in slots)
        {
            slot.UpdateUI();
        }
        goldText.text = gold.ToString();
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (item.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }

        foreach (var slot in slots) // It is the same item and there is room left
        {
            if (slot.item == item && slot.quantity < item.stackSize)
            {
                int availableSpace = item.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUI();

                if (quantity <= 0)
                    return;
            }
        }

        foreach (var slot in slots) // If items remain we will now look at the empty slots
        {
            if (slot.item == null)
            {
                int amountToAdd = Mathf.Min(item.stackSize - quantity);
                slot.item = item;
                slot.quantity = quantity;
                slot.UpdateUI();
                return;
            }
        }

        if (quantity > 0)
        {
            DropLoot(item, quantity);
        }
    }

    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.item, 1);
        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.item = null;
        }

        slot.UpdateUI();
    }

    private void DropLoot(ItemSO item, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(item, quantity);
    }

    public void UseItem(InventorySlot slot)
    {
        if (slot.item != null && slot.quantity >= 0)
        {
            useItem.ApplyItemEffect(slot.item);
            slot.quantity--;
            if (slot.quantity <= 0)
            {
                slot.item = null;
            }

            slot.UpdateUI();
        }
    }
}