using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunAchievementInteractor : UpgradeAchievementInteractorStrategy
{
    public HeavyGunAchievementInteractor(FirstTimePurcaseAcievementIDCollection collection) : base(collection)
    {
    }

    public override void ActivateFirstTimePurcaseAchievement()
    {
        Social.ReportProgress(this.firstTimePurcaseAcievementIds.HeavyWeapon, 100.0f, sucess =>
        {
            
        });
        
        Debug.Log("First heavy weapon purcased"); 
    }
}
