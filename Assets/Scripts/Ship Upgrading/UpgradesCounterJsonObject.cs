using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class UpgradesCounterJsonObject
{
    [SerializeField]
    private List<KeyValuePairWrapper<string, int>> serializedDictionary = new List<KeyValuePairWrapper<string, int>>();

    public UpgradesCounterJsonObject(Dictionary<ShipGunUpgrade, int> unSerializedDictionary)
    {
        foreach (KeyValuePair<ShipGunUpgrade, int> pair in unSerializedDictionary)
        {
            this.serializedDictionary.Add(new KeyValuePairWrapper<string, int>(pair.Key.name, pair.Value));
        }
    }

    public UpgradesCounterJsonObject()
    {

    }

    public Dictionary<ShipGunUpgrade, int> GenerateDictionaryFromJson()
    {
        Dictionary<ShipGunUpgrade, int> dictionary = new();

        foreach (var keyValuePair in this.serializedDictionary)
        {
            ShipGunUpgrade key = Resources.Load<ShipGunUpgrade>("ShipUpgrades/" + keyValuePair.key);
            int value = keyValuePair.value;

            dictionary.Add(key, value);
        }

        return dictionary;
    }

    public void SaveData(TextAsset saveFile)
    {
        string saveFilePath = AssetDatabase.GetAssetPath(saveFile);
        saveFilePath = saveFilePath.Substring(6);

        File.WriteAllText(Application.dataPath + saveFilePath, JsonUtility.ToJson(this, true));

        Debug.Log("Saved Upgrades Counter");
    }

    public void GenerateDefaultSafeFile(TextAsset jsonSaveFile)
    {
        Dictionary<ShipGunUpgrade, int> dictionary = new();
        var shipGunUpgrades = Resources.LoadAll<ShipGunUpgrade>("ShipUpgrades");
        foreach (ShipGunUpgrade upgrade in shipGunUpgrades)
        {
            dictionary.Add(upgrade, 0);
        }

        this.GenerateSaveableObject(dictionary).SaveData(jsonSaveFile);
    }

    public UpgradesCounterJsonObject GenerateSaveableObject(Dictionary<ShipGunUpgrade, int> dictionary)
    {
        UpgradesCounterJsonObject obj = new UpgradesCounterJsonObject(dictionary);
        return obj;
    }
}

