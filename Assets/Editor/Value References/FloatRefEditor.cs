using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloatReference))]
public class FloatRefEditor : Editor
{
    private FloatReference reference;
    public void OnEnable()
    {
        this.reference = (FloatReference)target;
    }

    public override void OnInspectorGUI()
    {
        this.reference.SetValue(EditorGUILayout.FloatField("Value", (float)this.reference.GetValue()));
    }
}
