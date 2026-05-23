using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffect(ItemSO item)
    {
        if(item.currentHealth > 0)
            StatManager.Instance.UpdateHealth(item.currentHealth);
        if(item.maxHealth > 0)
            StatManager.Instance.UpdateMaxHealth(item.maxHealth);
        if(item.speed > 0)
            StatManager.Instance.UpdateSpeed(item.speed);
        if (item.duration > 0)
        {
            StartCoroutine((EffectTimer(item, item.duration)));
        }
    }

    private IEnumerator EffectTimer(ItemSO item, float duration)
    {
        yield return new WaitForSeconds(duration);
        if(item.currentHealth > 0)
            StatManager.Instance.UpdateHealth(item.currentHealth);
        if(item.maxHealth > 0)
            StatManager.Instance.UpdateMaxHealth(item.maxHealth);
        if(item.speed > 0)
            StatManager.Instance.UpdateSpeed(item.speed);
    }
}
