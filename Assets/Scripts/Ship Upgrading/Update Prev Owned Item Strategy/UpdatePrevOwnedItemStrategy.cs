using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpdatePrevOwnedItemStrategy
{
    protected ShipPartLabel label;
    protected EquipItem equipItemAction;
    public UpdatePrevOwnedItemStrategy(ShipPartLabel label, EquipItem equipItemAction)
    {
        this.label = label;
        this.equipItemAction = equipItemAction;
    }
    public abstract void Execute();
}
