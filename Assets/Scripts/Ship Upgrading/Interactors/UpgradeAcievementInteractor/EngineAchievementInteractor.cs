using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class EngineAchievementInteractor : UpgradeAchievementInteractorStrategy
{
    public EngineAchievementInteractor(FirstTimePurcaseAcievementIDCollection collection) : base(collection)
    {
    }

    public override void ActivateFirstTimePurcaseAchievement()
    {
        Social.ReportProgress(this.firstTimePurcaseAcievementIds.Engine, 100.0f, sucess =>
        {
            
        });
        
        Debug.Log("First engine system purcased"); 
    }
}
