using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10;
    [SerializeField] float speed = 3.5f;
    [SerializeField] float turnSpeed = 5;

    NavMeshAgent navMeshAgent;
    EnemyHealth enemyHealth;
    CapsuleCollider collider;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        collider = GetComponent<CapsuleCollider>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (!enemyHealth.IsAlive)
        {
            enabled = false;
            collider.enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

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

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void EngageTarget()
    {
        FaceTarget();
        
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
