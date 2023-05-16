using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float speedBase;
    [SerializeField] protected float stoppingDistance;
    [SerializeField] protected float rotationSpeedAnglesPerSec;

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
            direction = player.position - transform.position;

            Rotate();
            Move();
        }
    }

    public virtual void Move()
    {
        RB.velocity = transform.forward * speedBase;
    }

    public virtual void Rotate()
    {
        targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeedAnglesPerSec * Time.deltaTime);
    }
}
