using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    private void ProcessDeath()
    {
        Debug.Log("You are dead...");
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        // TODO: add animation for being hit.

        if (hitPoints <= 0)
        {
            ProcessDeath();
        }
    }
}
