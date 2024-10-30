using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoolReference))]
public class BoolRefEditor : Editor
{
    private BoolReference reference;
    public void OnEnable()
    {
        this.reference = (BoolReference)target;
    }

    public override void OnInspectorGUI()
    {
        this.reference.SetValue(EditorGUILayout.Toggle("Value", this.reference.GetValue()));
    }
}
