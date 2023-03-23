using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSphere : MonoBehaviour
{
    public int dashDamage = 15;
    [SerializeField] AudioSource audiofx;

    IsKillable tempKillable;

    private void OnEnable()
    {
        audiofx.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        tempKillable = other.GetComponent<IsKillable>();

        if (tempKillable != null)
        {
            tempKillable.TakeDamage(dashDamage, 2);
        }

        tempKillable = null;
    }
}
