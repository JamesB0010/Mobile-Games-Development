using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystemsGunStorer : ScriptableObject
{
    [SerializeField] private Gun gunToStore;


    public Gun GetStoredGun()
    {
        return gunToStore;
    }

    public void SetStoredGun(Gun gun)
    {
        this.gunToStore = gun;
    }
}
