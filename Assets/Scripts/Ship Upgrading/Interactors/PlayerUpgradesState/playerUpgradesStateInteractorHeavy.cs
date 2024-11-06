using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUpgradesStateInteractorHeavy : PlayerUpgradesStateInteractorStrategy
{
    public playerUpgradesStateInteractorHeavy(PlayerUpgradesState playerUpgradesState) : base(playerUpgradesState)
    {
    }

    public override bool IsEqualTo(ShipItemUpgrade otherUpgrade, int index)
    {
        return this.playerUpgradesState.HeavyGuns[index] == otherUpgrade;
    }
}
