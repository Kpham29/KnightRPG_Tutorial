using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSo;
    public SpriteRenderer sr;
    public Animator anim;

    public bool canBePickedUp = true;
    public int quantity;
    public static event Action<ItemSO, int> OnItemLooted;

    private void OnValidate()
    {
        if (itemSo == null)
            return;
        
        UpdateAppearance();
    }

    public void Initialize(ItemSO item, int quantity)
    {
        this.itemSo = item;
        this.quantity = quantity;
        canBePickedUp = false;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        sr.sprite = itemSo.icon;
        this.name = itemSo.itemName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canBePickedUp == true)
        {
            Debug.Log("pickup");
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSo, quantity);
            Destroy(gameObject, .5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canBePickedUp = true;
        }
    }
}
