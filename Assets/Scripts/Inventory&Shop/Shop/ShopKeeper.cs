using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;
    
    public Animator animator;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;
    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopArmours;

    [SerializeField] private Camera shopkeeperCam;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -1);

    private bool playerInRange;
    private bool isShopOpen;

    public static event Action<ShopManager, bool> OnShopStateChanged;

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    currentShopKeeper = this;
                    isShopOpen = true;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                    
                    shopkeeperCam.transform.position = transform.position + cameraOffset;
                    shopkeeperCam.gameObject.SetActive(true);
                    
                    OpenItemShop();
                }
            }

            else if (Input.GetButtonDown("Cancel"))
            {
                Time.timeScale = 1;
                currentShopKeeper = null;
                isShopOpen = false;
                OnShopStateChanged?.Invoke(shopManager, false);
                shopCanvasGroup.alpha = 0;
                shopCanvasGroup.blocksRaycasts = false;
                shopCanvasGroup.interactable = false;
                
                shopkeeperCam.gameObject.SetActive(false);
            }
        }
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }

    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopWeapons);
    }

    public void OpenArmourShop()
    {
        shopManager.PopulateShopItems(shopArmours);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}