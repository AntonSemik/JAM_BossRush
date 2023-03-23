using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNode : MonoBehaviour
{
    [SerializeField] Transform[] upgrades;
    int activeUpgrade = 0;

    private void OnEnable()
    {
        activeUpgrade = Random.Range(0, upgrades.Length);
        upgrades[activeUpgrade].gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        upgrades[activeUpgrade].gameObject.SetActive(false);
    }
}
