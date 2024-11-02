using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Gun Upgrades/Light Ship Gun")]
public class LightItemUpgrade : ShipItemUpgrade
{
    [SerializeField] private LightGun gun;
    public LightGun Gun => this.gun;

    public override object GetUpgrade()
    {
        return this.gun;
    }
}
