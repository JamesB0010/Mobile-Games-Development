using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipLightItemInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipLightItemInteractorStrategy(EquipItem equipItemAction) : base(equipItemAction)
    {
    }

    public override void UpdatePrevOwned(int index)
    {
        this.equipItemAction.UpdatePreviouslyOwnedLightWeapon(index);
    }

    public override void SaveItemAction(UpgradeCell upgradeCell)
    {
        this.equipItemAction.SaveLightWeaponAction(upgradeCell);
    }
}