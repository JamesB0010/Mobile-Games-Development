using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipEngineInteractorStrategy : EquipItemInteractorStrategy
{
    public EquipEngineInteractorStrategy(EquipItem equipItemAction) : base(equipItemAction)
    {
    }

    public override void UpdatePrevOwned(int index)
    {
        this.equipItemAction.UpdatePreviouslyOwnedEngine();
    }

    public override void SaveItemAction(UpgradeCell upgradeCell)
    {
        this.equipItemAction.SaveEngineAction(upgradeCell);
    }
}