using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorLight : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorLight(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateItemDetailsText(int index = 0)
    {
        PlayerUpgradesState playerUpgradesState = this.ui.PlayerUpgradesState;
        LightGun lightGun = playerUpgradesState.LightGuns[index].Gun;
        this.ui.SetItemStatsUi(lightGun);
    }

    public override void UpdateUi(ShipItem item)
    {
        this.ui.UpdateUiLightGun((LightGun)item);
    }
}
