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

    [SerializeField] private TextAsset lightWeaponConfigurationSaveFile;

    [SerializeField] private TextAsset heavyWeaponConfigurationSaveFile;

    [SerializeField] private TextAsset OwnedUpgradesCounterJsonSaveFile;

    [MenuItem("Custom/Reset Scriptable Objects and Save Files")]
    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void ResetObjects()
    {
        ResetScriptableObjects instance = Resources.Load<ResetScriptableObjects>("Reset Scriptable Objects");

        instance.weaponsState.ResetLightGuns();

        instance.weaponsState.ResetHeavyGuns();

        SaveLightWeaponsStateJsonFile(instance);

        SaveHeavyWeaponsStateJsonFile(instance);

        new UpgradesCounterJsonObject().GenerateDefaultSafeFile(instance.OwnedUpgradesCounterJsonSaveFile);
    }

    private static void SaveLightWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(instance.weaponsState.LightGuns);
        string jsonString = JsonUtility.ToJson(weapons, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.lightWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.lightWeaponConfigurationSaveFile);
    }

    private static void SaveHeavyWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(instance.weaponsState.HeavyGuns);
        string jsonString = JsonUtility.ToJson(weapons, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.heavyWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.heavyWeaponConfigurationSaveFile);
    }
}