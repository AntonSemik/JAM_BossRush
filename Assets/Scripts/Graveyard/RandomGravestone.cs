using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGravestone : MonoBehaviour
{
    [SerializeField] GameObject[] gravestones;

    private void Start()
    {
        Instantiate(gravestones[Random.Range(0, gravestones.Length)], transform.position, transform.rotation);
        Destroy(this);
    }
}
