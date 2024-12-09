using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCounterInteractorHeavy : UpgradesCounterInteractorStrategy
{
    public UpgradeCounterInteractorHeavy(OwnedUpgradesCounter upgradesCounter) : base(upgradesCounter)
    {
    }

    public override void NotifyEquip(EquipItem itemEquipAction)
    {
        this.upgradesCounter.OnHeavyUpgradeEquipped(itemEquipAction);
    }

    public override int GetOwnedUpgradeTypeCount()
    {
        return this.upgradesCounter.GetCountsOfType<HeavyGunUpgrade>();
    }
}
