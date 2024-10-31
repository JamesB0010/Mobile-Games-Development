using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Gun Upgrades/Heavy Ship Gun")]
public class HeavyGunUpgrade : ShipGunUpgrade
{
    [SerializeField] private HeavyGun gun;
    public HeavyGun Gun => this.gun;

    public override object GetGun()
    {
        return gun;
    }
}
