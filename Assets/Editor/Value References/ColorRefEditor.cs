using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorReference))]
public class ColorRefEditor : Editor
{
    private ColorReference reference;
    public void OnEnable()
    {
        this.reference = (ColorReference)target;
    }

    public override void OnInspectorGUI()
    {
        this.reference.SetValue(EditorGUILayout.ColorField("Color", this.reference.GetValue()));
    }
}
