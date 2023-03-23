using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NightsSurvivedUIListener : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Awake()
    {
        DayNightCycle.updateNightCountUI += UpdateUI;
    }

    private void OnDestroy()
    {
        DayNightCycle.updateNightCountUI -= UpdateUI;
    }

    private void UpdateUI(int count)
    {
        text.text = "Nights survived: " + count.ToString();
    }
}
