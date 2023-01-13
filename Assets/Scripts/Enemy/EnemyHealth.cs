using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

    void Start()
    {
        GetComponent<Animator>().SetBool("dead", false);
    }

    public void ProcessDeath()
    {
        if (!isAlive) return;
        isAlive = false;
        GetComponent<Animator>().SetBool("dead", true);
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            ProcessDeath();
        }

        BroadcastMessage("OnDamageTaken");
    }
}
