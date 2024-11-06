using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterInteractorEnergySystem : UpgradesCounterInteractorStrategy
{
    public CounterInteractorEnergySystem(OwnedUpgradesCounter upgradesCounter) : base(upgradesCounter)
    {
    }


    public override void NotifyEquip(EquipItem itemEquipAction)
    {
        this.upgradesCounter.OnEnergySystemEquipped(itemEquipAction);
    }
}
