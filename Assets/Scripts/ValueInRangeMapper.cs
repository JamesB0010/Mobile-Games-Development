using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueInRangeMapper : MonoBehaviour
{
   public static float Map(float value, float oldMin, float oldMax, float newMin, float newMax)
   {
      return newMin + ((newMax - newMin) / (oldMax - oldMin)) * (value - oldMin);
   } 
}
