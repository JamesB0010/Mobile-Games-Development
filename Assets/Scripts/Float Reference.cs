using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Float")]
public class FloatReference : ScriptableObjectValueReference
{
    private float value;

    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(object val)
    {
        this.value = (float)val;
    }
    
    public static FloatReference operator + (FloatReference to, float val){
        to.SetValue((float)to.GetValue() + val);
        return to;
    }
}

#if UNITY_EDITOR

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

#endif