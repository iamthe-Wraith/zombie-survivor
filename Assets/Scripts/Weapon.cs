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
    [Range(1, 10)]
    [Tooltip("Fire rate is the number of rounds this weapons can fire per second.")]
    int fireRate = 2;

    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammo;

    private float delayBetweenRounds;
    private bool isFiring = false;
    public bool IsFiring { get { return isFiring; } }
    private bool firingIsPaused = false;
    private ParticleSystem[] muzzleFlashParticles;

    void Start()
    {
        delayBetweenRounds = 1f / (float)fireRate;
        muzzleFlashParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void CeaseFire()
    {
        isFiring = false;
    }

    public void Shoot()
    {
        if (firingIsPaused || ammo.CurrentAmmo <= 0) return;
        isFiring = true;

        ammo.ReduceCurrentAmmo();

        StartCoroutine("ProcessShoot");
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 2f);
    }

    private IEnumerator ProcessShoot()
    {
        while(isFiring)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
            {
                foreach(ParticleSystem muzzleFlashParticle in muzzleFlashParticles)
                {
                    muzzleFlashParticle.Play();
                }

                CreateHitImpact(hit);

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
