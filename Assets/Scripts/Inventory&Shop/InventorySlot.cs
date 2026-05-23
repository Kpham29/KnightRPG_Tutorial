using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO item;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;
    
    private InventoryManager inventoryManager;
    private static ShopManager activeShop;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    private void OnEnable()
    {
        ShopManager.OnShopStateChanged += HandleShopStateChanged;
    }

    private void OnDisable()
    {
        ShopManager.OnShopStateChanged -= HandleShopStateChanged;
    }

    private void HandleShopStateChanged(ShopManager shopManager, bool isOpen)
    {
        activeShop = isOpen ? shopManager : null;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (activeShop != null)
                {
                    activeShop.SellItem(item);
                    quantity--;
                    UpdateUI();
                }
                else
                {
                   if(StatManager.Instance.currentHealth >= StatManager.Instance.maxHealth && item.currentHealth > 0)
                                       return;
                                   inventoryManager.UseItem(this); 
                }
                
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if (quantity <= 0)
            item = null;
        if (item != null)
        {
            itemImage.sprite = item.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
