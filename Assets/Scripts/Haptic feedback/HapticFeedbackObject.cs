using System.Collections;
using System.Collections.Generic;
using CandyCoded.HapticFeedback;
using UnityEngine;

[CreateAssetMenu]
public class HapticFeedbackObject : ScriptableObject
{
    public void LightVibration()
    {
        HapticFeedback.LightFeedback();
    }
    
    public void MediumVibration()
    {
        HapticFeedback.MediumFeedback();
    }
    
    public void HeavyVibration()
    {
        HapticFeedback.HeavyFeedback();
    }
}
