using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipEnergySystemInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipEnergySystemInteractorStrategy(ShipPartLabel label, EquipItem equipItemAction) : base(label, equipItemAction)
    {
    }

    public override void UpdatePrevOwned()
    {
        this.equipItemAction.UpdatePreviouslyOwnedEnergySystem();
    }
}