using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ResetScriptableObjects : ScriptableObject
{
    [FormerlySerializedAs("weaponsState")] [SerializeField] private PlayerUpgradesState upgradesState;

    [SerializeField] private TextAsset lightWeaponConfigurationSaveFile;

    [SerializeField] private TextAsset heavyWeaponConfigurationSaveFile;

    [SerializeField] private TextAsset armourConfigurationSaveFile;

    [SerializeField] private TextAsset engineConfigurationSaveFile;

    [SerializeField] private TextAsset energySystemConfigurationSaveFile;

    [SerializeField] private TextAsset OwnedUpgradesCounterJsonSaveFile;

    [SerializeField] private FloatReference playerMoney;
    
    [MenuItem("Custom/Reset Scriptable Objects and Save Files")]
    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void ResetObjects()
    {
        ResetScriptableObjects instance = Resources.Load<ResetScriptableObjects>("Reset Scriptable Objects");

        instance.upgradesState.ResetLightGuns();

        instance.upgradesState.ResetHeavyGuns();

        instance.upgradesState.ResetArmour();

        instance.upgradesState.ResetEngine();

        instance.upgradesState.ResetEnergySystem();

        SaveLightWeaponsStateJsonFile(instance);

        SaveHeavyWeaponsStateJsonFile(instance);

        SaveArmourStateJsonFile(instance);

        SaveEngineStateJsonFile(instance);

        SaveEnergySystemStateJsonFile(instance);
        
        instance.playerMoney.SetValue(0);
        
        new UpgradesCounterJsonObject().GenerateDefaultSafeFile(instance.OwnedUpgradesCounterJsonSaveFile);
    }

    private static void SaveLightWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(instance.upgradesState.LightGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.lightWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.lightWeaponConfigurationSaveFile);
    }

    private static void SaveHeavyWeaponsStateJsonFile(ResetScriptableObjects instance)
    {
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(instance.upgradesState.HeavyGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.heavyWeaponConfigurationSaveFile).Substring(6);
        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.heavyWeaponConfigurationSaveFile);
    }

    private static void SaveArmourStateJsonFile(ResetScriptableObjects instance)
    {
        SavedUpgradesJsonObject armour = new SavedUpgradesJsonObject(new List<ShipItemUpgrade>()
            { instance.upgradesState.ArmourAbstract });

        string jsonString = JsonUtility.ToJson(armour, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.armourConfigurationSaveFile).Substring(6);

        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.armourConfigurationSaveFile);
    }

    private static void SaveEngineStateJsonFile(ResetScriptableObjects instance)
    {
        SavedUpgradesJsonObject engine = new SavedUpgradesJsonObject(new List<ShipItemUpgrade>()
            { instance.upgradesState.EngineAbstract });

        string jsonString = JsonUtility.ToJson(engine, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.engineConfigurationSaveFile).Substring(6);

        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.engineConfigurationSaveFile);
    }

    private static void SaveEnergySystemStateJsonFile(ResetScriptableObjects instance)
    {
        SavedUpgradesJsonObject energySystem = new SavedUpgradesJsonObject(new List<ShipItemUpgrade>()
            { instance.upgradesState.EnergySystemAbstract });

        string jsonString = JsonUtility.ToJson(energySystem, true);
        string saveFilePath = Application.dataPath + AssetDatabase.GetAssetPath(instance.energySystemConfigurationSaveFile).Substring(6);

        File.WriteAllText(saveFilePath, jsonString);
        AssetDatabase.SaveAssetIfDirty(instance.energySystemConfigurationSaveFile);
    }
}