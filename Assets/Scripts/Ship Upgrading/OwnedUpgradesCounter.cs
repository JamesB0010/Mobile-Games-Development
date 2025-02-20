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

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [SerializeField] private FirstTimePurcaseAcievementIDCollection firstTimePurcaseAchievementIDs;
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

        TextAsset ownedUpgrades = BuzzardGameData.OwnedUpgradesConfigFile;
        UpgradesCounterJsonObject obj = JsonUtility.FromJson<UpgradesCounterJsonObject>(ownedUpgrades.text);

        this.upgradesCount = obj.GenerateDictionaryFromJson();
    }

    //on upgrade purchased
    public void IncrementUpgradeCount(SelectedCellHighlight highlight)
    {
        ShipItemUpgrade upgrade = cellHighlight.SelectedCell.Upgrade;
        if (this.IsFirstPurcasedItemOfThisType(upgrade))
            upgrade.generateUpgradeAcievementInteractor(this.firstTimePurcaseAchievementIDs).ActivateFirstTimePurcaseAchievement();
        
        this.upgradesCount[upgrade]++;
        this.SaveToJson();
    }

    public int GetCountsOfType<T>() where T:  ShipItemUpgrade
    {
        int count = 0;
        foreach (var upgrade in this.upgradesCount)
        {
            if (upgrade.Key is T)
            {
                count += upgrade.Value;
            }
        }

        return count;
    }

    private bool IsFirstPurcasedItemOfThisType(ShipItemUpgrade upgrade)
    { 
        int quantityOfTypePurcased = upgrade.GenerateUpgradeCounterInteractor(this).GetOwnedUpgradeTypeCount();
        return quantityOfTypePurcased == 0;
    } 

    public void OnLightUpgradeEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        if(itemEquipper.PreviouslyOwnedLightWeapon != null)
            this.upgradesCount[itemEquipper.PreviouslyOwnedLightWeapon]++;
        
        this.SaveToJson();
    }

    public void OnHeavyUpgradeEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        if(itemEquipper.PreviouslyOwnedHeavyWeapon != null)
            this.upgradesCount[itemEquipper.PreviouslyOwnedHeavyWeapon]++;
        
        this.SaveToJson();
    }

    public void OnArmourEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        if(itemEquipper.PreviouslyOwnedArmour != null)
            this.upgradesCount[itemEquipper.PreviouslyOwnedArmour]++;
        
        this.SaveToJson();
    }

    public void OnEnergySystemEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        if(itemEquipper.PreviouslyownedEnergySystem != null)
            this.upgradesCount[itemEquipper.PreviouslyownedEnergySystem]++;
        
        this.SaveToJson();
    }

    public void OnEngineEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        if(itemEquipper.PreviouslyOwnedEngine != null)
            this.upgradesCount[itemEquipper.PreviouslyOwnedEngine]++ ;
        
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
        GenerateSaveableObject().SaveData(BuzzardGameData.OwnedUpgradesConfigFile);
    }

    public float GetItemInCirculationCount(ShipItemUpgrade shipItemUpgrade)
    {
        switch (shipItemUpgrade)
        {
            case LightGunUpgrade lightGun:
            {
                List<ShipItemUpgrade> itemList = this.playerUpgradesState.LightGunsAbstract.FindAll(item => item == shipItemUpgrade);
                float itemCount = this.GetUpgradeCount(lightGun);
                itemCount += itemList.Count;
                return itemCount;
            }
            case HeavyGunUpgrade heavyGun:
            {
                List<ShipItemUpgrade> itemList = this.playerUpgradesState.HeavyGunsAbstract.FindAll(item => item == shipItemUpgrade);
                float itemCount = this.GetUpgradeCount(heavyGun);
                itemCount += itemList.Count;
                return itemCount;
            }
            case ArmourUpgrade armour:
                bool armourEquipped = this.playerUpgradesState.Armour == armour;
                return  armourEquipped ? 1 : this.GetUpgradeCount(armour);
            case EnergySystemsUpgrade energy:
                bool energySystemEquipped = this.playerUpgradesState.EnergySystem == energy;
                return energySystemEquipped ? 1 : this.GetUpgradeCount(energy);
            case EngineUpgrade engine:
                bool engineEquipped = this.playerUpgradesState.Engine == engine;
                return engineEquipped ? 1 : this.GetUpgradeCount(engine);
        }
        
        return 0;
    }
}

