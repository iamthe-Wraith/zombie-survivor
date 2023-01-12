using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        
        Ammo ammo = other.GetComponent<Ammo>();

        if (ammo != null)
        {
            ammo.IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
