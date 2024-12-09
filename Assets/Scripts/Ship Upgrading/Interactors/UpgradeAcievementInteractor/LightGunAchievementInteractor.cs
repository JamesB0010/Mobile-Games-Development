using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGunAchievementInteractor: UpgradeAchievementInteractorStrategy
{
    public LightGunAchievementInteractor(FirstTimePurcaseAcievementIDCollection collection) : base(collection)
    {
    }

    public override void ActivateFirstTimePurcaseAchievement()
    {
        Social.ReportProgress(this.firstTimePurcaseAcievementIds.LigtWeapon, 100.0f, sucess => { });

        Debug.Log("First Light gun purcased");
    }
}
