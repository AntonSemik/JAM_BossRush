using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speedBase;
    [SerializeField] float stoppingDistance;
    [SerializeField] float rotationSpeedAnglesPerSec;

    Vector3 direction;
    Quaternion targetRot;
    Rigidbody RB;
    Transform player;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        player = PlayerTransform.instance.transform;
    }

    private void Update()
    {
        if (player != null)
        {
            Move();
        }
    }

    private void Move()
    {
        direction = player.position - transform.position;

        targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeedAnglesPerSec * Time.deltaTime);

        RB.velocity = transform.forward * speedBase;
    }
}
