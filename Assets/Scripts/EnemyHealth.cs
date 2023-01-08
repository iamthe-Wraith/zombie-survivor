using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    public void ProcessDeath()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            ProcessDeath();
        }
    }
}
