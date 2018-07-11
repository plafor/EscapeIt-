using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSettingsScript : MonoBehaviour {

    public Toggle activated;
    public Toggle acquired;
    public Dropdown masteryLevel;

    void Start()
    {
        //display settings as they are saved
    }
    public void activate()
    {
        if (!activated.isOn)
        {
            masteryLevel.interactable = false;
        }else
        {
            masteryLevel.interactable = true;
        }
    }

    public void acquire()
    {
        if (acquired.isOn)
        {
            masteryLevel.interactable = false;
        }
        else
        {
            masteryLevel.interactable = true;
        }
    }

    public void changeMastery()
    {
        
        if (masteryLevel.value == 4)
        {
            acquired.interactable = true;
        }
        else
        {
            acquired.interactable = false;
        }
    }
}
