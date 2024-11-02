using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Platters/Energy System Upgrade Platter")]
public class EnergySystemUpgradesPlatter : ShipUpgradesPlatterBase
{
    [SerializeField] private EnergySystemsUpgrade[] upgrades;
    public override ShipItemUpgrade[] GetShipUpgrades()
    {
        return this.upgrades;
    }
}
