using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipEnergySystemInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipEnergySystemInteractorStrategy(EquipItem equipItemAction) : base(equipItemAction)
    {
    }

    public override void UpdatePrevOwned(int index)
    {
        this.equipItemAction.UpdatePreviouslyOwnedEnergySystem();
    }

    public override void SaveItemAction(UpgradeCell upgradeCell)
    {
        this.equipItemAction.SaveEnergySystemAction(upgradeCell);
    }
}