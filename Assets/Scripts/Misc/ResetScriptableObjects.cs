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

    [SerializeField] private TextAsset armourConfigurationSaveFile;

    [SerializeField] private TextAsset engineConfigurationSaveFile;

    [SerializeField] private TextAsset energySystemConfigurationSaveFile;

    [SerializeField] private TextAsset OwnedUpgradesCounterJsonSaveFile;

    [MenuItem("Custom/Reset Scriptable Objects and Save Files")]
    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void ResetObjects()
    {
        ResetScriptableObjects instance = Resources.Load<ResetScriptableObjects>("Reset Scriptable Objects");

        instance.weaponsState.ResetLightGuns();

        instance.weaponsState.ResetHeavyGuns();

        instance.weaponsState.ResetArmour();

        instance.weaponsState.ResetEngine();

        instance.weaponsState.ResetEnergySystem();

        SaveLightWeaponsStateJsonFile(instance);

        SaveHeavyWeaponsStateJsonFile(instance);

        SaveArmourStateJsonFile(instance);

        SaveEngineStateJsonFile(instance);

        SaveEnergySystemStateJsonFile(instance);

        new UpgradesCounterJsonObject().GenerateDefaultSafeFile(instance.OwnedUpgradesCounterJsonSaveFile);
    }

    private static void SaveLightWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(instance.weaponsState.LightGunsAbstract);
        string jsonString = JsonUtility.ToJson(weapons, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.lightWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.lightWeaponConfigurationSaveFile);
    }

    private static void SaveHeavyWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(instance.weaponsState.HeavyGunsAbstract);
        string jsonString = JsonUtility.ToJson(weapons, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.heavyWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.heavyWeaponConfigurationSaveFile);
    }

    private static void SaveArmourStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject armour = new SavedWeaponsJsonObject(new List<ShipItemUpgrade>()
            { instance.weaponsState.ArmourAbstract });

        string jsonString = JsonUtility.ToJson(armour, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.armourConfigurationSaveFile).Substring(6);

        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.armourConfigurationSaveFile);
    }

    private static void SaveEngineStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject engine = new SavedWeaponsJsonObject(new List<ShipItemUpgrade>()
            { instance.weaponsState.EngineAbstract });

        string jsonString = JsonUtility.ToJson(engine, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.engineConfigurationSaveFile).Substring(6);

        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.engineConfigurationSaveFile);
    }

    private static void SaveEnergySystemStateJsonFile(ResetScriptableObjects instance)
    {
        SavedWeaponsJsonObject energySystem = new SavedWeaponsJsonObject(new List<ShipItemUpgrade>()
            { instance.weaponsState.EnergySystemAbstract });

        string jsonString = JsonUtility.ToJson(energySystem, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.energySystemConfigurationSaveFile).Substring(6);

        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.energySystemConfigurationSaveFile);
    }
}