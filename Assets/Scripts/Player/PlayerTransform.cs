using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public static PlayerTransform instance;

    private void Awake()
    {
        instance = this;
    }
}
