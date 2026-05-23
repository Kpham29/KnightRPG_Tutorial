using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StatManager.Instance.UpdateMaxHealth(10);
                break;
            case "Sword Slash":
                playerCombat.enabled = true;
                break;
            default:
                Debug.LogWarning("Unknown skill: "+skillName);
                break;
        }
    }
}
