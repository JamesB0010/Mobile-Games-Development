using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasedEventTimeKeeper
{
    //Attributes
    private float timeBetweenEvents;

    private float lastEventTimestamp = -100.0f;


    public PhasedEventTimeKeeper(float timeBetweenEvents)
    {
        this.timeBetweenEvents = timeBetweenEvents;
    }
    
    public bool HasEnoughTimeElapsedSinceEvent()
    {
        return Time.timeSinceLevelLoad - lastEventTimestamp > timeBetweenEvents;
    }

    public void UpdateTimestamp()
    {
        this.lastEventTimestamp = Time.timeSinceLevelLoad;
    }

    public void Prime()
    {
        this.lastEventTimestamp = -100.0f;
    }
}
