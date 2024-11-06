using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Engine")]
public class EngineUpgrade : ShipItemUpgrade
{
    [SerializeField] private Engine engine;
    public Engine Engine => this.engine;
    public override object GetUpgrade()
    {
        return engine;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(EquipItem itemEquipAction)
    {
        return new EquipEngineInteractorStrategy(itemEquipAction);
    }

    public override UiUpdaterInteractorStrategy GenerateUiUpdatorInteractor(UIViewUpdater ui)
    {
        return new UiUpdaterInteractorEngine(ui);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradeCounterInteractorEngine(upgradesCounter);
    }

    public override PlayerUpgradesStateInteractorStrategy GenerateUpgradesStateInteractor(PlayerUpgradesState playerUpgradesState)
    {
        return new playerUpgradesStateInteractorEngine(playerUpgradesState);
    }
}
