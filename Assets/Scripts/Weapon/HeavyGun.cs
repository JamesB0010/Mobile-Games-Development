using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Guns/Heavy Gun")]
public class HeavyGun : Gun
{
    [SerializeField]
    private float maxAmmoCount;

    public float MaxAmmoCount => this.maxAmmoCount;

    private float currentAmmoCount;
    

    public bool AbleToShoot => this.ableToShoot;
   

    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}

