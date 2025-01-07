using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Heavy Ship Gun")]
public class HeavyGunUpgrade : ShipItemUpgrade
{
    [SerializeField] private HeavyGun gun;
    public HeavyGun Gun => this.gun;

    public override ShipItem GetUpgrade()
    {
        return gun;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(EquipItem itemEquipAction)
    {
        return new EquipHeavyItemInteractorStrategy(itemEquipAction);
    }

    public override UiUpdaterInteractorStrategy GenerateUiUpdatorInteractor(UIViewUpdater ui)
    {
        return new UiUpdaterInteractorHeavy(ui);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradeCounterInteractorHeavy(upgradesCounter);
    }

    public override PlayerUpgradesStateInteractorStrategy GenerateUpgradesStateInteractor(PlayerUpgradesState playerUpgradesState)
    {
        return new playerUpgradesStateInteractorHeavy(playerUpgradesState);
    }

    public override UpgradeAchievementInteractorStrategy generateUpgradeAcievementInteractor(
        FirstTimePurcaseAcievementIDCollection acievementIDCollection)
    {
        return new HeavyGunAchievementInteractor(acievementIDCollection);
    }
}
