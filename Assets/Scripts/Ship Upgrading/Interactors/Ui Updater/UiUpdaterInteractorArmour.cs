using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorArmour : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorArmour(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateItemDetailsText(int index = 0)
    {
        PlayerUpgradesState upgradesState = this.ui.PlayerUpgradesState;
        Armour armour = upgradesState.Armour.Armour;
        this.ui.SetItemStatsUi(armour);
    }

    public override void UpdateUi(ShipItem item)
    {
        this.ui.UpdateUiArmour((Armour)item);
    }

    public override void UpdateItemToPurchaseStats(ShipItem item)
    {
        Armour armour = (Armour)item;
        this.ui.UpdateUiArmourToPurchase(armour.ItemName, 
            armour.EnergyUsedPerHit.ToString(), 
            armour.MinimumOperationalEnergyLevel.ToString(), 
            ((1 - armour.DamageDampeningMultiplier) * 100).ToString());
    }
}
