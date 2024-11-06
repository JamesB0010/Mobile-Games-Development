using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUpgradesStateInteractorEnergySystem : PlayerUpgradesStateInteractorStrategy
{
    public playerUpgradesStateInteractorEnergySystem(PlayerUpgradesState playerUpgradesState) : base(playerUpgradesState)
    {
    }

    public override bool IsEqualTo(ShipItemUpgrade otherUpgrade, int index)
    {
        return this.playerUpgradesState.EnergySystem == otherUpgrade;
    }
}
