using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Armour")]
public class ArmourUpgrade : ShipItemUpgrade
{
    [SerializeField] private Armour armour;

    public Armour Armour => this.armour;

    public override object GetUpgrade()
    {
        return armour;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(ShipPartLabel label, EquipItem itemEquipAction)
    {
        return new EquipArmourInteractorStrategy(label, itemEquipAction);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradesCounterInteractorArmour(upgradesCounter);
    }
}
