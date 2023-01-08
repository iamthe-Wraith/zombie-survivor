using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField]
    [Tooltip("How far this weapon can shoot/reach and still deal damage.")]
    float range = 100f;

    [SerializeField]
    [Tooltip("Is the amount of damage done to a target for each round that hits it.")]
    float damage = 1f;

    [SerializeField]
    [Tooltip("Fire rate is the number of rounds this weapons can fire per second.")]
    float fireRate = 2f;

    private float delayBetweenRounds;
    private bool isFiring = false;
    private bool firingIsPaused = false;
    public bool IsFiring { get { return isFiring; } }

    void Start()
    {
        delayBetweenRounds = 1f / fireRate;
    }

    public void CeaseFire()
    {
        isFiring = false;
    }

    public void Shoot()
    {
        if (firingIsPaused) return;

        isFiring = true;

        StartCoroutine("ProcessShoot");
    }

    private IEnumerator ProcessShoot()
    {
        while(isFiring)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
            {
                Debug.Log($"You hit a(n) {hit.transform.name}");
                // TODO: add some hit effect for visual players

                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target)
                {
                    target.TakeDamage(damage);
                }
            }

            firingIsPaused = true;
            yield return new WaitForSeconds(delayBetweenRounds);
            firingIsPaused = false;
        }
    }
}
