using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeAchievementInteractorStrategy
{
    protected FirstTimePurcaseAcievementIDCollection firstTimePurcaseAcievementIds;

    public UpgradeAchievementInteractorStrategy(FirstTimePurcaseAcievementIDCollection collection)
    {
        this.firstTimePurcaseAcievementIds = collection;
    }

    public abstract void ActivateFirstTimePurcaseAchievement();
}
