using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Platters/Heavy Gun Upgrade Platter")]
public class ShipHeavyUpgradesPlatter : ShipUpgradesPlatterBase
{
    [SerializeField] private HeavyGunUpgrade[] upgrades;

    public override ShipItemUpgrade[] GetShipUpgrades()
    {
        return upgrades;
    }
}
