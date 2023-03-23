using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRewards : MonoBehaviour
{
    [SerializeField] GameObject[] rewards;

    public delegate void OnUpgrade(string value);
    public static OnUpgrade onUpgrade;

    private void Awake()
    {
        DayNightCycle.dayStart += ActivateRewards;
        DayNightCycle.nightStart += DisableRewards;

        onUpgrade += DisableRewardsString;
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= ActivateRewards;
        DayNightCycle.nightStart -= DisableRewards;

        onUpgrade -= DisableRewardsString;
    }

    void DisableRewards()
    {
        foreach (GameObject go in rewards)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }
    }

    void DisableRewardsString(string value)
    {
        DisableRewards();
    }

    void ActivateRewards()
    {
        foreach (GameObject go in rewards)
        {
            go.SetActive(true);
        }
    }
}
