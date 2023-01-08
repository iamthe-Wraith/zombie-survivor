using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }

        if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;   
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireSphere(transform.position, chaseRange);    
    }

    private void AttackTarget()
    {
        Debug.Log("Haiya!");
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (navMeshAgent.stoppingDistance < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }
}
