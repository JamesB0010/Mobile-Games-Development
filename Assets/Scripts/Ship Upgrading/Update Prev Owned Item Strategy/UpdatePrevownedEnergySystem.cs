using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePrevownedEnergySystem : UpdatePrevOwnedItemStrategy
{
    public UpdatePrevownedEnergySystem(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void Execute()
    {
        this.equipItemAction.UpdatePreviouslyOwnedEnergySystem();
    }
}