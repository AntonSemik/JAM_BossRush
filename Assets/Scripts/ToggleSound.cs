using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("soundIsOn", 1) == 1)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void ToggleSoundOnOff(bool toggleTo)
    {
        if (toggleTo)
        {
            PlayerPrefs.SetInt("soundIsOn", 1);

            AudioListener.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("soundIsOn", 0);

            AudioListener.volume = 0;
        }
    }
}
