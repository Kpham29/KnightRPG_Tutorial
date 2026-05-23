using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthAnimator;

    private void Start()
    {
        healthText.text = "HP: " + StatManager.Instance.currentHealth + " / " + StatManager.Instance.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        StatManager.Instance.currentHealth -= amount;
        healthAnimator.Play("TextUpdate");
        healthText.text = "HP: " + StatManager.Instance.currentHealth + " / " + StatManager.Instance.maxHealth;
        if (StatManager.Instance.currentHealth <= 0) gameObject.SetActive(false);
    }
}