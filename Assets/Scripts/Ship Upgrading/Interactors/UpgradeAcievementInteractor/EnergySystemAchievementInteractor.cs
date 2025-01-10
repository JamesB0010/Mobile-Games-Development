using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystemAchievementInteractor : UpgradeAchievementInteractorStrategy
{
    public EnergySystemAchievementInteractor(FirstTimePurcaseAcievementIDCollection collection) : base(collection)
    {
    }

    public override void ActivateFirstTimePurcaseAchievement()
    {
        Social.ReportProgress(this.firstTimePurcaseAcievementIds.EnergySystem, 100.0f, sucess =>
        {
        });
        
        Debug.Log("First energy system purcased");
    }
}
