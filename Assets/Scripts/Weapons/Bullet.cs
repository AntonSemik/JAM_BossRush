using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float startVelocity;

    public int damage;

    [SerializeField] float lifeTime = 3f;
    float timeLeft;

    [SerializeField] Rigidbody RB;

    IsKillable tempKillable;

    private void OnEnable()
    {
        RB.velocity = transform.forward * startVelocity;
        timeLeft = lifeTime;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tempKillable = other.GetComponent<IsKillable>();

        if(tempKillable != null)
        {
            tempKillable.TakeDamage(damage, 1);
        }

        tempKillable = null;

        gameObject.SetActive(false);
    }
}
