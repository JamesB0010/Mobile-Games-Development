using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Armour")]
public class ArmourUpgrade : ShipItemUpgrade
{
    [SerializeField] private Armour armour;

    public Armour Armour => this.armour;

    public override ShipItem GetUpgrade()
    {
        return armour;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(EquipItem itemEquipAction)
    {
        return new EquipArmourInteractorStrategy(itemEquipAction);
    }

    public override UiUpdaterInteractorStrategy GenerateUiUpdatorInteractor(UIViewUpdater ui)
    {
        return new UiUpdaterInteractorArmour(ui);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradesCounterInteractorArmour(upgradesCounter);
    }

    public override PlayerUpgradesStateInteractorStrategy GenerateUpgradesStateInteractor(PlayerUpgradesState playerUpgradesState)
    {
        return new playerUpgradesStateInteractorArmour(playerUpgradesState);
    }

    public override UpgradeAchievementInteractorStrategy generateUpgradeAcievementInteractor(
        FirstTimePurcaseAcievementIDCollection acievementIDCollection)
    {
        return new ArmourAchievementInteractor(acievementIDCollection);
    }
}
