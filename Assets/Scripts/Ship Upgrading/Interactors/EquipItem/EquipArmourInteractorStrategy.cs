using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipArmourInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipArmourInteractorStrategy(EquipItem equipItemAction) : base(equipItemAction)
    {
    }

    public override void UpdatePrevOwned(int index = 0)
    {
        this.equipItemAction.UpdatePreviouslyOwnedArmour();
    }

    public override void SaveItemAction(UpgradeCell upgradeCell)
    {
        this.equipItemAction.SaveArmourAction(upgradeCell);
    }
}
