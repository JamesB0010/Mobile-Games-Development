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

    public UpgradesCounterJsonObject(Dictionary<ShipItemUpgrade, int> unSerializedDictionary)
    {
        foreach (KeyValuePair<ShipItemUpgrade, int> pair in unSerializedDictionary)
        {
            this.serializedDictionary.Add(new KeyValuePairWrapper<string, int>(pair.Key.name, pair.Value));
        }
    }

    public UpgradesCounterJsonObject()
    {

    }

    public Dictionary<ShipItemUpgrade, int> GenerateDictionaryFromJson()
    {
        Dictionary<ShipItemUpgrade, int> dictionary = new();

        foreach (var keyValuePair in this.serializedDictionary)
        {
            ShipItemUpgrade key = Resources.Load<ShipItemUpgrade>("ShipUpgrades/" + keyValuePair.key);
            int value = keyValuePair.value;

            dictionary.Add(key, value);
        }

        return dictionary;
    }

    public void SaveData(TextAsset saveFile)
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", saveFile.name + ".txt"), JsonUtility.ToJson(this, true));
    }

    public void GenerateDefaultSafeFile(TextAsset jsonSaveFile)
    {
        Dictionary<ShipItemUpgrade, int> dictionary = new();
        var shipGunUpgrades = Resources.LoadAll<ShipItemUpgrade>("ShipUpgrades");
        foreach (ShipItemUpgrade upgrade in shipGunUpgrades)
        {
            dictionary.Add(upgrade, 0);
        }

        this.GenerateSaveableObject(dictionary).SaveData(jsonSaveFile);
    }

    public UpgradesCounterJsonObject GenerateSaveableObject(Dictionary<ShipItemUpgrade, int> dictionary)
    {
        UpgradesCounterJsonObject obj = new UpgradesCounterJsonObject(dictionary);
        return obj;
    }
}

