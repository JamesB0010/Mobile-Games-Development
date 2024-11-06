using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Heavy Ship Gun")]
public class HeavyGunUpgrade : ShipItemUpgrade
{
    [SerializeField] private HeavyGun gun;
    public HeavyGun Gun => this.gun;

    public override object GetUpgrade()
    {
        return gun;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(ShipPartLabel label, EquipItem itemEquipAction)
    {
        return new EquipHeavyItemInteractorStrategy(label, itemEquipAction);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradeCounterInteractorHeavy(upgradesCounter);
    }
}
