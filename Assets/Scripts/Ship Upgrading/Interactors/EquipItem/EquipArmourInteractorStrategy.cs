using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipArmourInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipArmourInteractorStrategy(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void UpdatePrevOwned()
    {
        this.equipItemAction.UpdatePreviouslyOwnedArmour();
    }
}
