using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Energy System")]
public class EnergySystemsUpgrade : ShipItemUpgrade
{
    [SerializeField] private EnergySystem energySystem;
    public EnergySystem EnergySystem => this.energySystem;

    public override object GetUpgrade()
    {
        return this.energySystem;
    }

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(EquipItem itemEquipAction)
    {
        return new EquipEnergySystemInteractorStrategy(itemEquipAction);
    }

    public override UiUpdaterInteractorStrategy GenerateUiUpdatorInteractor(UIViewUpdater ui)
    {
        return new UiUpdaterInteractorEnergySystem(ui);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new CounterInteractorEnergySystem(upgradesCounter);
    }

    public override PlayerUpgradesStateInteractorStrategy GenerateUpgradesStateInteractor(PlayerUpgradesState playerUpgradesState)
    {
        return new playerUpgradesStateInteractorEnergySystem(playerUpgradesState);
    }
}
