using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] AmmoType ammoType;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammo;

    private StarterAssetsInputs _input;
    private float delayBetweenRounds;
    private bool isFiring = false;
    public bool IsFiring { get { return isFiring; } }
    private bool firingIsPaused = false;
    public bool FiringIsPaused { get { return firingIsPaused; } }
    private ParticleSystem[] muzzleFlashParticles;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        delayBetweenRounds = 1f / (float)fireRate;
        muzzleFlashParticles = GetComponentsInChildren<ParticleSystem>();
    }

    void OnEnable() {
        firingIsPaused = false;
        isFiring = false;
    }

    void OnDisable() {
        firingIsPaused = true;
        isFiring = true;
    }

    public void CeaseFire()
    {
        firingIsPaused = false;
        isFiring = false;
    }

    public void Shoot()
    {
        if (firingIsPaused || ammo.GetCurrentAmmo(ammoType) <= 0) return;
        isFiring = true;

        ammo.ReduceCurrentAmmo(ammoType);

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
