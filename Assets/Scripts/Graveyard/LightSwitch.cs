using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] GameObject dayLight;
    [SerializeField] GameObject nightLight;

    private void Start()
    {
        DayNightCycle.dayStart += SwitchToDay;
        DayNightCycle.nightStart += SwitchToNight;
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= SwitchToDay;
        DayNightCycle.nightStart -= SwitchToNight;
    }

    void SwitchToDay()
    {
        dayLight.SetActive(true);
        nightLight.SetActive(false);
    }

    void SwitchToNight()
    {
        dayLight.SetActive(false);
        nightLight.SetActive(true);
    }
}
