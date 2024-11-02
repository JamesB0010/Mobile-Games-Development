using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Platters/Engine Upgrade Platter")]
public class EngineUpgradesPlatter : ShipUpgradesPlatterBase
{
    [SerializeField] private EngineUpgrade[] upgrades;
    public override ShipItemUpgrade[] GetShipUpgrades()
    {
        return this.upgrades;
    }
}
