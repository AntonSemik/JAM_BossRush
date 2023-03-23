using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public delegate void TakeBullet();
    public static TakeBullet takeBullet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (takeBullet != null) takeBullet();
            gameObject.SetActive(false);
        }
    }
}
