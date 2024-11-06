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
}
