using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePrevOwnedHeavy : UpdatePrevOwnedItemStrategy
{
    public UpdatePrevOwnedHeavy(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void Execute()
    {
        this.equipItemAction.UpdatePreviouslyOwnedHeavyWeapon(this.label.WeaponIndex);
    }
}