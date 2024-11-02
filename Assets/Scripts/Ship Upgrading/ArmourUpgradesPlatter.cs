using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Platters/Armour Upgrade Platter")]
public class ArmourUpgradesPlatter : ShipUpgradesPlatterBase
{
    [SerializeField] private ArmourUpgrade[] upgrades;
    public override ShipItemUpgrade[] GetShipUpgrades()
    {
        return this.upgrades;
    }
}
