using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePrevOwnedEngine : UpdatePrevOwnedItemStrategy
{
    public UpdatePrevOwnedEngine(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void Execute()
    {
        this.equipItemAction.UpdatePreviouslyOwnedEngine();
    }
}