using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Light Ship Gun")]
public class LightGunUpgrade : ShipItemUpgrade
{
    [SerializeField] private LightGun gun;
    public LightGun Gun => this.gun;

    public override object GetUpgrade()
    {
        return this.gun;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(EquipItem itemEquipAction)
    {
        return new EquipLightItemInteractorStrategy(itemEquipAction);
    }

    public override UiUpdaterInteractorStrategy GenerateUiUpdatorInteractor(UIViewUpdater ui)
    {
        return new UiUpdaterInteractorLight(ui);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradeCounterInteractorLight(upgradesCounter);
    }

    public override PlayerUpgradesStateInteractorStrategy GenerateUpgradesStateInteractor(PlayerUpgradesState playerUpgradesState)
    {
        return new playerUpgradesStateInteractorLight(playerUpgradesState);
    }
}
