using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopSlot[] shopSlots;
    [SerializeField] private InventoryManager inventoryManager;
    

    public void PopulateShopItems(List<ShopItems> shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.item, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemSO item, int price)
    {
        if (item != null && inventoryManager.gold >= price)
        {
            if (HasSpaceForItem(item))
            {
                inventoryManager.gold -= price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                inventoryManager.AddItem(item, 1);
            }
        }
    }

    private bool HasSpaceForItem(ItemSO item)
    {
        foreach (var slot in  inventoryManager.slots)
        {
            if(slot.item == item && slot.quantity < item.stackSize)
                return true;
            else if(slot.item == null)
                return true;
        }
        return false;
    }

    public void SellItem(ItemSO item)
    {
        if(item == null)
            return;
        foreach (var slot in shopSlots)
        {
            if (slot.item == item)
            {
                inventoryManager.gold += (slot.price/2);
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }
}

[System.Serializable]
    public class ShopItems
    {
        public ItemSO item;
        public int price;
    }
