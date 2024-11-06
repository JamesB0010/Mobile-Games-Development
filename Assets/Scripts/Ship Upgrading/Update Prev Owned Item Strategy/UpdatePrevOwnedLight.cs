using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePrevOwnedLight : UpdatePrevOwnedItemStrategy
{
    public UpdatePrevOwnedLight(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void Execute()
    {
        this.equipItemAction.UpdatePreviouslyOwnedLightWeapon(this.label.WeaponIndex);
    }
}