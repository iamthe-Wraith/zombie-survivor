using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int currentAmmo = 10;
    public int CurrentAmmo { get { return currentAmmo; } }

    public void ReduceCurrentAmmo()
    {
        currentAmmo -= 1;
    }
}
