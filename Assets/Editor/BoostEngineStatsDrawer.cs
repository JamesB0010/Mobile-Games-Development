using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EngineBoostStats))]
public class BoostEngineStatsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty boostEnabledProperty = property.FindPropertyRelative("canBoost");

        SerializedProperty boostDrainRateProperty = property.FindPropertyRelative("energyBoostDrainRate");

        boostEnabledProperty.boolValue = EditorGUILayout.Toggle("Boost Enabled", boostEnabledProperty.boolValue);

        if (boostEnabledProperty.boolValue)
        {
            boostDrainRateProperty.floatValue =
                EditorGUILayout.FloatField("Boosting Energy Drain Rate", boostDrainRateProperty.floatValue);
        }
    }
}
