using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EquipItem : ItemShopAction
{
    public event Action SelectedCellEquipped;


    [SerializeField] private TextAsset lightWeaponConfigurationSaveFile;

    [SerializeField] private TextAsset heavyWeaponConfigurationSaveFile;

    [SerializeField] private TextAsset armourConfigurationSaveFile;

    [SerializeField] private TextAsset energySystemConfigurationFile;

    [SerializeField] private TextAsset engineConfigutationFile;


    private ShipItemUpgrade previouslyOwnedLightWeapon;
    public ShipItemUpgrade PreviouslyOwnedLightWeapon => this.previouslyOwnedLightWeapon;

    private ShipItemUpgrade previouslyOwnedHeavyWeapon;

    public ShipItemUpgrade PreviouslyOwnedHeavyWeapon => this.previouslyOwnedHeavyWeapon;

    private ShipItemUpgrade previouslyOwnedArmour;

    public ShipItemUpgrade PreviouslyOwnedArmour => this.previouslyOwnedArmour;

    private ShipItemUpgrade previouslyOwnedEnergySystem;
    public ShipItemUpgrade PreviouslyownedEnergySystem => this.previouslyOwnedEnergySystem;

    private ShipItemUpgrade previouslyOwnedEngine;

    public ShipItemUpgrade PreviouslyOwnedEngine => this.previouslyOwnedEngine;

    public void EquipCell(UpgradeCell cell)
    {
        SaveToJson(cell);
    }
    public void SaveLightWeaponAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditLightWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(this.playerUpgradesState.LightGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        
        
        File.WriteAllText(Application.dataPath + "/Resources/Json/lightWeaponConfiguration.txt", jsonString);
        #if UNITY_EDITOR
        AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
        #endif
    }

    public void SaveHeavyWeaponAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditHeavyWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(this.playerUpgradesState.HeavyGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        File.WriteAllText(Application.dataPath + "/Resources/Json/heavyWeaponConfiguration.txt", jsonString);
        #if UNITY_EDITOR
        AssetDatabase.SaveAssetIfDirty(this.heavyWeaponConfigurationSaveFile);
        #endif
    }

    public void SaveEngineAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditEngine((EngineUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.EngineAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Application.dataPath + "/Resources/Json/engineConfiguration.txt",jsonString);
        #if UNITY_EDITOR
        AssetDatabase.SaveAssetIfDirty(this.engineConfigutationFile);
        #endif
    }

    public void SaveEnergySystemAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditEnergySystem((EnergySystemsUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.EnergySystemAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Application.dataPath + "/Resources/Json/energySystemConfiguration.txt", jsonString);
        #if UNITY_EDITOR
        AssetDatabase.SaveAssetIfDirty(this.energySystemConfigurationFile);
        #endif
    }

    public void SaveArmourAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditArmour((ArmourUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.ArmourAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Application.dataPath + "/Resources/Json/armourConfiguration.txt", jsonString);
        #if UNITY_EDITOR
        AssetDatabase.SaveAssetIfDirty(this.armourConfigurationSaveFile);
        #endif
    }

    private void SaveToJson(UpgradeCell cell)
    {
        var interactor = cell.Upgrade.GenerateEquipItemInteractor(this);
        interactor.UpdatePrevOwned(cell.WeaponIndex);
        interactor.SaveItemAction(cell);
        
        this.SelectedCellEquipped?.Invoke();
    }

    public void UpdatePreviouslyOwnedLightWeapon(int weaponIndex)
    {
        this.previouslyOwnedLightWeapon = base.playerUpgradesState.LightGuns[weaponIndex];
    }
    public void UpdatePreviouslyOwnedHeavyWeapon(int weaponIndex)
    {
        this.previouslyOwnedHeavyWeapon = base.playerUpgradesState.HeavyGuns[weaponIndex];
    }

    public void UpdatePreviouslyOwnedArmour()
    {
        this.previouslyOwnedArmour = base.playerUpgradesState.Armour;
    }

    public void UpdatePreviouslyOwnedEnergySystem()
    {
        this.previouslyOwnedEnergySystem = base.playerUpgradesState.EnergySystem;
    }

    public void UpdatePreviouslyOwnedEngine()
    {
        this.previouslyOwnedEngine = base.playerUpgradesState.Engine;
    }
}
