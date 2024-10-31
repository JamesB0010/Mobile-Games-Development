using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade Platters/Light Gun Upgrade Platter")]
public class ShipLightGunUpgradesPlatter : ShipUpgradesPlatterBase
{
    [SerializeField] private LightGunUpgrade[] upgrades;

    public override ShipGunUpgrade[] GetShipUpgrades()
    {
        return upgrades;
    }
}
