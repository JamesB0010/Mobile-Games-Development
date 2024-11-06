using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorEngine : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorEngine(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateItemDetailsText(int index = 0)
    {
        PlayerUpgradesState upgradesState = this.ui.PlayerUpgradesState;
        Engine engine = upgradesState.Engine.Engine;
        this.ui.SetItemStatsUi(engine);
    }

    public override void UpdateUi(ShipItem item)
    {
        this.ui.UpdateUiEngine((Engine)item);
    }
}
