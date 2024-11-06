using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipLightItemInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipLightItemInteractorStrategy(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void UpdatePrevOwned()
    {
        this.equipItemAction.UpdatePreviouslyOwnedLightWeapon(this.label.WeaponIndex);
    }
}