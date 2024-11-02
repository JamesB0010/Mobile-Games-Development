using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade Platters/Light Gun Upgrade Platter")]
public class ShipLightGunUpgradesPlatter : ShipUpgradesPlatterBase
{
    [SerializeField] private LightItemUpgrade[] upgrades;

    public override ShipItemUpgrade[] GetShipUpgrades()
    {
        return upgrades;
    }
}
