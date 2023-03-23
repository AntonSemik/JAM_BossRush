using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsKillable : MonoBehaviour
{
    [SerializeField] GameObject damageZone;
    [SerializeField] Collider bodyCollider;

    [SerializeField] int maxHealth;
    private int currentHealth;

    [SerializeField] bool isBleeding;
    [SerializeField] protected ParticleSystem bleedingVFX;

    private void Start()
    {
        damageZone.SetActive(true);
        bodyCollider.enabled = true;
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        damageZone.SetActive(true);
        bodyCollider.enabled = true;
        currentHealth = maxHealth;
    }


    public void TakeDamage(int value, int source)
    {
        if (currentHealth <= 0) return;

        currentHealth -= value;

        if (isBleeding)
        {
            bleedingVFX.Play();
        }

        if (currentHealth <= 0)
        {
            if(source == 0)
            {
                AmmoDrops.dropAmmo(transform.position);
            }

            StartCoroutine(DieWithDelay(0.2f));
        }
    }

    IEnumerator DieWithDelay(float delay)
    {
        bodyCollider.enabled = false;
        damageZone.SetActive(false);

        yield return new WaitForSeconds(delay);

        Die();
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
