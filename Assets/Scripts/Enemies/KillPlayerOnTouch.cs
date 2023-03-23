using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    [SerializeField] string targetTag;

    IsKillable tempKillable;

    private void OnTriggerEnter(Collider other)
    {
        tempKillable = other.GetComponent<IsKillable>();

        if (tempKillable != null)
        {
            tempKillable.TakeDamage(1, 999);
        }

        tempKillable = null;
    }
}
