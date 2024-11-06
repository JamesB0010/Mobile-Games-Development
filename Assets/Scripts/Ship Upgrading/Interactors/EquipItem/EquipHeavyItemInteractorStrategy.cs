using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHeavyItemInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipHeavyItemInteractorStrategy(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void UpdatePrevOwned()
    {
        this.equipItemAction.UpdatePreviouslyOwnedHeavyWeapon(this.label.WeaponIndex);
    }
}