using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipItemInteractorStrategy
{
    protected ShipPartLabel label;
    protected EquipItem equipItemAction;
    public EquipItemInteractorStrategy(ShipPartLabel label, EquipItem equipItemAction)
    {
        this.label = label;
        this.equipItemAction = equipItemAction;
    }
    public abstract void UpdatePrevOwned();
}
