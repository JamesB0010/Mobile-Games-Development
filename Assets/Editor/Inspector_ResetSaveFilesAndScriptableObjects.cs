using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ResetScriptableObjects))]
public class Inspector_ResetSaveFilesAndScriptableObjects : Editor
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
            ResetScriptableObjects.WipeSaveData();
        }
    }
}
