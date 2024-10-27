using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetScriptableObjects : ScriptableObject
{
    [SerializeField] private PlayerWeaponsState weaponsState;

    [MenuItem("Custom/Reset Scriptable Objects")]
    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void ResetObjects()
    {
        ResetScriptableObjects instance = Resources.Load<ResetScriptableObjects>("Reset Scriptable Objects");
        
        instance.weaponsState.ResetLightGuns();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ResetScriptableObjects))]
public class ResetScriptableObjectsEditor : Editor
{
    private ResetScriptableObjects castedTarget;

    private void OnEnable()
    {
        this.castedTarget = (ResetScriptableObjects)this.target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        if (GUILayout.Button("Reset Scriptable Objects"))
        {
            ResetScriptableObjects.ResetObjects();
        }
    }
}
#endif