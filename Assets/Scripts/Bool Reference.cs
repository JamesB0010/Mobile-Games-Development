using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Boolean")]
public class BoolReference : ScriptableObjectValueReference
{
    private bool value;

    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(object val)
    {
        Debug.Log("Set bool reference " + name + " Value: " + Convert.ToBoolean(val));
        this.value = (bool)val;
    }
}

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
        this.reference.SetValue(EditorGUILayout.Toggle("Value", (bool)this.reference.GetValue()));
    }
}
