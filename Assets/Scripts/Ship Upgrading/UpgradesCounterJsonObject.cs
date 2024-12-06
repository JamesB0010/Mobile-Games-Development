using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class UpgradesCounterJsonObject
{
    //TODO when equipping items make it save to save game
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
        File.WriteAllText(Application.dataPath + "/Resources/Json/" + saveFile.name + ".txt", JsonUtility.ToJson(this, true));
        
        #if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
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

