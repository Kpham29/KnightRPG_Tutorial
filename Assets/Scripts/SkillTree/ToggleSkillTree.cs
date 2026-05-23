using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup skillCanvas;
    private bool skillTreeOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                skillCanvas.alpha = 0;
                skillCanvas.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                skillCanvas.alpha = 1;
                skillCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }
    }
}
