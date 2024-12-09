using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCounterInteractorLight : UpgradesCounterInteractorStrategy
{
    public UpgradeCounterInteractorLight(OwnedUpgradesCounter upgradesCounter) : base(upgradesCounter)
    {
    }

    public override void NotifyEquip(EquipItem itemEquipAction)
    {
        this.upgradesCounter.OnLightUpgradeEquipped(itemEquipAction);
    }

    public override int GetOwnedUpgradeTypeCount()
    {
        return this.upgradesCounter.GetCountsOfType<LightGunUpgrade>();
    }
}
