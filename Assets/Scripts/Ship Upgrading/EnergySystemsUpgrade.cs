using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Energy System")]
public class EnergySystemsUpgrade : ShipItemUpgrade
{
    [SerializeField] private EnergySystem energySystem;

    public override object GetUpgrade()
    {
        return this.energySystem;
    }
}
