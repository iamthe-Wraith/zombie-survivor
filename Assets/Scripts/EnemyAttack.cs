using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float damage = 5f;

    void Start()
    {
        
    }

    public void AttackHitEvent()
    {
        if (target == null) return;

        Debug.Log($"bang bang! you took {damage} hp of damage...");
    }
}
