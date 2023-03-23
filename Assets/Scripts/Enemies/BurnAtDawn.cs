using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurnAtDawn : MonoBehaviour
{
    [SerializeField] ParticleSystem burningVFX;
    [SerializeField] float burningDeathDelay;

    bool burning = false;
    float burningDeathTimer;

    private void Start()
    {
        DayNightCycle.dayStart += Burn;
    }

    private void Update()
    {
        if (!burning) return;

        burningDeathTimer -= Time.deltaTime;

        if (burningDeathTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        burning = false;
        burningVFX.Pause();
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= Burn;
    }

    void Burn()
    {
        if (!gameObject.activeSelf) return;

        burning = true;
        burningDeathTimer = burningDeathDelay;

        burningVFX.Play();
    }

}
