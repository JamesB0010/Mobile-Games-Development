using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntReference))]
public class IntRefEditor : Editor
{
    private IntReference reference;
    public void OnEnable()
    {
        this.reference = (IntReference)target;
    }

    public override void OnInspectorGUI()
    {
        this.reference.SetValue(EditorGUILayout.IntField("Value", this.reference.GetValue()));
    }
}
