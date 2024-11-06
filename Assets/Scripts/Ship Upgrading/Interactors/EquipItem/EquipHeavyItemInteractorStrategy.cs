using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHeavyItemInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipHeavyItemInteractorStrategy(EquipItem equipItemAction) : base(equipItemAction)
    {
    }

    public override void UpdatePrevOwned(int index)
    {
        this.equipItemAction.UpdatePreviouslyOwnedHeavyWeapon(index);
    }

    public override void SaveItemAction(UpgradeCell upgradeCell)
    {
        this.equipItemAction.SaveHeavyWeaponAction(upgradeCell);
    }
}