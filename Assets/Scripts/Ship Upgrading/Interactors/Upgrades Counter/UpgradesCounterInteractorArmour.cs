using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesCounterInteractorArmour : UpgradesCounterInteractorStrategy
{
    public UpgradesCounterInteractorArmour(OwnedUpgradesCounter upgradesCounter) : base(upgradesCounter)
    {
    }

    public override void NotifyEquip(EquipItem itemEquipAction)
    {
        this.upgradesCounter.OnArmourEquipped(itemEquipAction);
    }
}
