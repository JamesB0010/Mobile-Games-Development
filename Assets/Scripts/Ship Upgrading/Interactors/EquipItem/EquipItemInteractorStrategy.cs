using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipItemInteractorStrategy
{
    protected EquipItem equipItemAction;
    public EquipItemInteractorStrategy(EquipItem equipItemAction)
    {
        this.equipItemAction = equipItemAction;
    }
    public abstract void UpdatePrevOwned(int index = 0);

    public abstract void SaveItemAction(UpgradeCell upgradeCell);
}
