using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCounterInteractorEngine : UpgradesCounterInteractorStrategy
{
    public UpgradeCounterInteractorEngine(OwnedUpgradesCounter upgradesCounter) : base(upgradesCounter)
    {
    }

    public override void NotifyEquip(EquipItem itemEquipAction)
    {
        this.upgradesCounter.OnEngineEquipped(itemEquipAction);
    }
}
