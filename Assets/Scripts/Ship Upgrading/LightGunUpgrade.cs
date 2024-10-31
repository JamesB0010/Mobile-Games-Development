using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Gun Upgrades/Light Ship Gun")]
public class LightGunUpgrade : ShipGunUpgrade
{
    [SerializeField] private LightGun gun;
    public LightGun Gun => this.gun;

    public override object GetGun()
    {
        return this.gun;
    }
}
