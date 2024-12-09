using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourAchievementInteractor : UpgradeAchievementInteractorStrategy
{
    public ArmourAchievementInteractor(FirstTimePurcaseAcievementIDCollection collection) : base(collection)
    {
    }

    public override void ActivateFirstTimePurcaseAchievement()
    {
        Social.ReportProgress(this.firstTimePurcaseAcievementIds.Armour, 100.0f, sucess => { });

        Debug.Log("First armour purcased");
    }
}
