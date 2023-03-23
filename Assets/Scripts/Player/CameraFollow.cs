using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = PlayerTransform.instance.transform;
    }

    private void Update()
    {
        transform.Translate(playerTransform.position - transform.position);
    }
}
