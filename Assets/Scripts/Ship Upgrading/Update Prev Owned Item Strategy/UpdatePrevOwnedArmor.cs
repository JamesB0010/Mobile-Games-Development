using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePrevOwnedArmor : UpdatePrevOwnedItemStrategy
{
    public UpdatePrevOwnedArmor(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void Execute()
    {
        this.equipItemAction.UpdatePreviouslyOwnedArmour();
    }
}
