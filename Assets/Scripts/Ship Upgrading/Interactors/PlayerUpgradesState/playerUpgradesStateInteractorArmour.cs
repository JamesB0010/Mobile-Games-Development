using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUpgradesStateInteractorArmour : PlayerUpgradesStateInteractorStrategy
{
    public playerUpgradesStateInteractorArmour(PlayerUpgradesState playerUpgradesState) : base(playerUpgradesState)
    {
    }

    public override bool IsEqualTo(ShipItemUpgrade otherUpgrade, int index = 0)
    {
        return this.playerUpgradesState.Armour == otherUpgrade;
    }
}
