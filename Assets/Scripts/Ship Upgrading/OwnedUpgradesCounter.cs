using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
internal class UpgradesCounterJsonObject
{
    [SerializeField]
    private List<KeyValuePairWrapper<string, int>> serializedDictionary = new List<KeyValuePairWrapper<string, int>>();

    internal UpgradesCounterJsonObject(Dictionary<ShipGunUpgrade, int> unSerializedDictionary)
    {
        foreach(KeyValuePair<ShipGunUpgrade, int> pair in unSerializedDictionary)
        {
            this.serializedDictionary.Add(new KeyValuePairWrapper<string, int>(pair.Key.name, pair.Value));
        }
    }

    internal Dictionary<ShipGunUpgrade, int> GenerateDictionaryFromJson()
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

    internal void SaveData(TextAsset saveFile)
    {
        string saveFilePath = AssetDatabase.GetAssetPath(saveFile);
        saveFilePath = saveFilePath.Substring(6);
        
        File.WriteAllText(Application.dataPath + saveFilePath, JsonUtility.ToJson(this, true));
        
        Debug.Log("Saved Upgrades Counter");
    }
}

[Serializable]
internal struct KeyValuePairWrapper<T1, T2>
{
    [SerializeField] 
    public T1 key;

    [SerializeField] 
    public T2 value;

    public KeyValuePairWrapper(T1 key, T2 value)
    {
        this.key = key;
        this.value = value;
    }
}

public class OwnedUpgradesCounter : MonoBehaviour
{
    private static OwnedUpgradesCounter instance = null;
    public static OwnedUpgradesCounter Instance => OwnedUpgradesCounter.instance;
    
    internal Dictionary<ShipGunUpgrade, int> upgradesCount = new Dictionary<ShipGunUpgrade, int>();

    private SelectedCellHighlight cellHighlight;

    [SerializeField] internal TextAsset jsonSaveFile;
    private void Awake()
    {
        if (OwnedUpgradesCounter.instance == null)
            OwnedUpgradesCounter.instance = this;
        else
            Destroy(this.gameObject);
            
    }

    private void Start()
    {
        this.cellHighlight = FindObjectOfType<SelectedCellHighlight>();

        UpgradesCounterJsonObject obj = JsonUtility.FromJson<UpgradesCounterJsonObject>(this.jsonSaveFile.text);

        this.upgradesCount = obj.GenerateDictionaryFromJson();
    }

    private HashSet<T> GenerateHashSet<T>(T[] arrayIn)
    {
        HashSet<T> set = new HashSet<T>();
        foreach (T element in arrayIn)
        {
            set.Add(element);
        }

        return set;
    }


    //on upgrade purchased
    public void IncrementUpgradeCount(SelectedCellHighlight highlight)
    {
        this.upgradesCount[highlight.SelectedCell.Upgrade]++;
    }

    //on upgrade equipped
    public void OnUpgradeEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        this.upgradesCount[itemEquipper.PreviouslyOwnedLightWeapon]++;
    }

    public int GetUpgradeCount(ShipGunUpgrade upgrade)
    {
        return this.upgradesCount[upgrade];
    }

    internal UpgradesCounterJsonObject GenerateSaveableObject()
    {
        UpgradesCounterJsonObject obj = new UpgradesCounterJsonObject(this.upgradesCount);
        return obj;
    }

    internal UpgradesCounterJsonObject GenerateSaveableObject(Dictionary<ShipGunUpgrade, int> dictionary)
    {
        UpgradesCounterJsonObject obj = new UpgradesCounterJsonObject(dictionary);
        return obj;
    }

    internal void GenerateDefaultSafeFile()
    {
        Dictionary<ShipGunUpgrade, int> dictionary = new();
        var shipGunUpgrades= Resources.LoadAll<ShipGunUpgrade>("ShipUpgrades");
        foreach (ShipGunUpgrade upgrade in shipGunUpgrades)
        {
            dictionary.Add(upgrade, 0);
        }

        this.GenerateSaveableObject(dictionary).SaveData(this.jsonSaveFile);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OwnedUpgradesCounter))]
class OwnedUpgradesCounterCustomUI : Editor
{
    private OwnedUpgradesCounter castedTarget;
    private void OnEnable()
    {
        this.castedTarget = (OwnedUpgradesCounter)this.target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(15);

        if (GUILayout.Button("Print Stored Upgrades and Counts"))
        {
            Debug.Log("Upgrade Pair Counts:");
            if (this.castedTarget.upgradesCount.Count == 0)
                Debug.Log("No Upgrades stored");
                
            foreach (var upgradePair in this.castedTarget.upgradesCount)
            {
                Debug.Log("Upgrade Name: " + upgradePair.Key.name + " Count: " + upgradePair.Value);
            }
        }


        if (GUILayout.Button("Save state To json"))
        {
            this.castedTarget.GenerateSaveableObject().SaveData(this.castedTarget.jsonSaveFile);
        }


        if (GUILayout.Button("Generate Default Save File"))
        {
            this.castedTarget.GenerateDefaultSafeFile();
        }
    }
}

#endif
