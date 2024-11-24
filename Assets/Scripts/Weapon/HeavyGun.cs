using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Guns/Player/Heavy Gun")]
public class HeavyGun : Gun
{
    [SerializeField]
    private float maxAmmoCount;

    public float MaxAmmoCount => this.maxAmmoCount;

    [SerializeField] private float currentAmmoCount;

    public float CurrentAmmoCount
    {
        get => this.currentAmmoCount;
        set => this.currentAmmoCount = value;
    }


    public bool AbleToShoot => this.ableToShoot;
   

    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}

