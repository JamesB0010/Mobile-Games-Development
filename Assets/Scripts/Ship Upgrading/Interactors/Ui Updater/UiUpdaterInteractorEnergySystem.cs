using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorEnergySystem : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorEnergySystem(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateItemDetailsText(int index = 0)
    {
        PlayerUpgradesState upgradesState = this.ui.PlayerUpgradesState;
        EnergySystem eSystem = upgradesState.EnergySystem.EnergySystem;
        this.ui.SetItemStatsUi(eSystem);
    }
}
