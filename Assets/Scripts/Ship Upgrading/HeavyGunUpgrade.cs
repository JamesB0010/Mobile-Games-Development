using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Heavy Ship Gun")]
public class HeavyGunUpgrade : ShipItemUpgrade
{
    [SerializeField] private HeavyGun gun;
    public HeavyGun Gun => this.gun;

    public override object GetUpgrade()
    {
        return gun;
    }
}
