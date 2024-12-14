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

    public override void UpdateItemToPurchaseStats(ShipItem item)
    {
        Engine engine = (Engine)item;
        if(engine.EngineBoostStats.CanBoost)
            this.ui.UpdateUiEngineToPurchase(engine.ItemName, 
                engine.AccelerationSpeed.ToString(),
                engine.MaxVelocity.ToString(),
                engine.EnergyDrainRate.ToString(), 
                engine.EngineBoostStats.EnergyBoostDrainRate.ToString()
                );
        else
            this.ui.UpdateUiEngineToPurchase(engine.ItemName, 
                engine.AccelerationSpeed.ToString(),
                engine.MaxVelocity.ToString(),
                engine.EnergyDrainRate.ToString()
                );
    }
}
