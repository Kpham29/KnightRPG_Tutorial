using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ItemSO item;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;
    
    [SerializeField] private ShopManager shopManager;
    public int price;

    public void Initialize(ItemSO newItem, int price)
    {
        // fill the slot with information
        item = newItem;
        itemImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        this.price = price;
        priceText.text = price.ToString();
    }

    public void OnBuyButtonClicked()
    {
        shopManager.TryBuyItem(item, price);
    }
}
