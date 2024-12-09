using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradesCounterInteractorStrategy
{
    protected OwnedUpgradesCounter upgradesCounter;

    public UpgradesCounterInteractorStrategy(OwnedUpgradesCounter upgradesCounter)
    {
        this.upgradesCounter = upgradesCounter;
    }

    public abstract void NotifyEquip(EquipItem itemEquipAction);
    
    public abstract int GetOwnedUpgradeTypeCount();
}
