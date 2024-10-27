using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueInRangeMapper
{
    public static float MapRange(this float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return newMin + ((newMax - newMin) / (oldMax - oldMin)) * (value - oldMin);
    }
}
