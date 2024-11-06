using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerUpgradesStateInteractorStrategy
{
    protected PlayerUpgradesState playerUpgradesState;

    public PlayerUpgradesStateInteractorStrategy(PlayerUpgradesState playerUpgradesState)
    {
        this.playerUpgradesState = playerUpgradesState;
    }

    public abstract bool IsEqualTo(ShipItemUpgrade otherUpgrade, int index = 0);
}
