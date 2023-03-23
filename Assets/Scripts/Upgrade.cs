using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] string upgradeName = "Insert name";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (UpgradeRewards.onUpgrade != null) UpgradeRewards.onUpgrade(upgradeName);
        }
    }
}
