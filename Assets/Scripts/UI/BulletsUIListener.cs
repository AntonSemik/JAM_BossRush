using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletsUIListener : MonoBehaviour
{
    [SerializeField] TMP_Text UItext;

    private void Awake()
    {
        PlayerShoot.sendBulletsToUI += UpdateUI;
    }

    private void OnDestroy()
    {
        PlayerShoot.sendBulletsToUI -= UpdateUI;
    }

    void UpdateUI(int currentBullets, int maxBullets)
    {
        if (UItext == null) return;

        UItext.text = "Bullets: " + currentBullets.ToString() + "/" + maxBullets.ToString();
    }
}
