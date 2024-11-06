using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUpgradesStateInteractorLight : PlayerUpgradesStateInteractorStrategy
{
    public playerUpgradesStateInteractorLight(PlayerUpgradesState playerUpgradesState) : base(playerUpgradesState)
    {
    }

    public override bool IsEqualTo(ShipItemUpgrade otherUpgrade, int index = 0)
    {
        return this.playerUpgradesState.LightGuns[index] == otherUpgrade;
    }
}
