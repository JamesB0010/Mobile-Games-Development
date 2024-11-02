using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class OwnedUpgradesCounter : MonoBehaviour
{
    private static OwnedUpgradesCounter instance = null;
    public static OwnedUpgradesCounter Instance => OwnedUpgradesCounter.instance;

    private Dictionary<ShipItemUpgrade, int> upgradesCount = new Dictionary<ShipItemUpgrade, int>();
    public Dictionary<ShipItemUpgrade, int> UpgradesCount => this.upgradesCount;
    private SelectedCellHighlight cellHighlight;

    [SerializeField] private TextAsset jsonSaveFile;
    public TextAsset JsonSaveFile => this.jsonSaveFile;
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

    //on upgrade purchased
    public void IncrementUpgradeCount(SelectedCellHighlight highlight)
    {
        this.upgradesCount[highlight.SelectedCell.Upgrade]++;
        this.SaveToJson();
    }

    //on upgrade equipped
    public void OnUpgradeEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        this.upgradesCount[itemEquipper.PreviouslyOwnedLightWeapon]++;
        this.SaveToJson();
    }

    public int GetUpgradeCount(ShipItemUpgrade upgrade)
    {
        return this.upgradesCount[upgrade];
    }

    public UpgradesCounterJsonObject GenerateSaveableObject()
    {
        UpgradesCounterJsonObject obj = new UpgradesCounterJsonObject(this.upgradesCount);
        return obj;
    }
    public void SaveToJson()
    {

        GenerateSaveableObject().SaveData(this.jsonSaveFile);
    }
}

