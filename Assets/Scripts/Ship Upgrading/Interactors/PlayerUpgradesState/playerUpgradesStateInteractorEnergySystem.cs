using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUpgradesStateInteractorEngine : PlayerUpgradesStateInteractorStrategy
{
    public playerUpgradesStateInteractorEngine(PlayerUpgradesState playerUpgradesState) : base(playerUpgradesState)
    {
    }

    public override bool IsEqualTo(ShipItemUpgrade otherUpgrade, int index = 0)
    {
        return this.playerUpgradesState.EnergySystem == otherUpgrade;
    }
}
