using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorEnergySystem : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorEnergySystem(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateCurrentEquippedItemDetailsText(int index = 0)
    {
        PlayerUpgradesState upgradesState = this.ui.PlayerUpgradesState;
        EnergySystem eSystem = upgradesState.EnergySystem.EnergySystem;
        this.ui.SetItemStatsUi(eSystem);
    }

    public override void UpdateUi(ShipItem item)
    {
        this.ui.SetItemStatsUi((EnergySystem)item);
    }

    public override void UpdateItemToPurchaseStats(ShipItem item)
    {
        EnergySystem eSys = (EnergySystem)item;
        this.ui.UpdateUiEnergySystemToPurchase(eSys.ItemName, 
            eSys.MaxEnergy.ToString(),
            eSys.RechargeRate.ToString());
    }
}
