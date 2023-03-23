using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChurchClock : MonoBehaviour
{
    [SerializeField] Image clockImage;

    private void Awake()
    {
        DayNightCycle.updateClockUI += UpdateUI;
    }

    private void OnDestroy()
    {
        DayNightCycle.updateClockUI -= UpdateUI;
    }

    private void UpdateUI(float percent)
    {
        clockImage.fillAmount = percent;
    }
}
