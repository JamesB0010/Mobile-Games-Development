using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipEngineInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipEngineInteractorStrategy(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void UpdatePrevOwned()
    {
        this.equipItemAction.UpdatePreviouslyOwnedEngine();
    }
}