using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ResetScriptableObjects : ScriptableObject
{
    [SerializeField] private PlayerWeaponsState weaponsState;

    [FormerlySerializedAs("lightWeaponConfiguration")] [SerializeField] private TextAsset lightWeaponConfigurationSaveFile;

    [MenuItem("Custom/Reset Scriptable Objects")]
    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void ResetObjects()
    {
        ResetScriptableObjects instance = Resources.Load<ResetScriptableObjects>("Reset Scriptable Objects");
        
        instance.weaponsState.ResetLightGuns();
        
       SaveLightWeaponsStateJsonFile(instance);
    }

    private static void SaveLightWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedLightWeaponsJsonObject lightWeapons = new SavedLightWeaponsJsonObject(instance.weaponsState.LightGuns);
        string jsonString = JsonUtility.ToJson(lightWeapons, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.lightWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.lightWeaponConfigurationSaveFile);
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