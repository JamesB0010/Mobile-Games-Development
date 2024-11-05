using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
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
    private void SaveLightWeaponAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditLightWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(this.playerUpgradesState.LightGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.lightWeaponConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
    }

    private void SaveHeavyWeaponAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditHeavyWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(this.playerUpgradesState.HeavyGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.heavyWeaponConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.heavyWeaponConfigurationSaveFile);
    }

    private void SaveEngineAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditEngine((EngineUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.EngineAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.engineConfigutationFile).Substring(6),jsonString);
        AssetDatabase.SaveAssetIfDirty(this.engineConfigutationFile);
    }

    private void SaveEnergySystemAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditEnergySystem((EnergySystemsUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.EnergySystemAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.energySystemConfigurationFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.energySystemConfigurationFile);
    }

    private void SaveArmourAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditArmour((ArmourUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.ArmourAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.armourConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.armourConfigurationSaveFile);
    }

    private void SaveToJson(UpgradeCell cell)
    {
        switch (cell.ShipSection)
        {
            case ShipSections.lightWeapons:
                this.UpdatePreviouslyOwnedLightWeapon(cell.WeaponIndex);
                this.SaveLightWeaponAction(cell);
                this.SelectedCellEquipped?.Invoke();
                break;
            case ShipSections.heavyWeapons:
                this.UpdatePreviouslyOwnedHeavyWeapon(cell.WeaponIndex);
                this.SaveHeavyWeaponAction(cell);
                this.SelectedCellEquipped?.Invoke();
                break;
            case ShipSections.armour:
                this.UpdatePreviouslyOwnedArmour();
                this.SaveArmourAction(cell);
                this.SelectedCellEquipped?.Invoke();
                break;
            case ShipSections.engine:
                this.UpdatePreviouslyOwnedEngine();
                this.SaveEngineAction(cell);
                this.SelectedCellEquipped?.Invoke();
                break;
            case ShipSections.energy:
                this.UpdatePreviouslyOwnedEnergySystem();
                this.SaveEnergySystemAction(cell);
                this.SelectedCellEquipped?.Invoke();
                break;
            default:
                break;
        }
    }

    public void UpdatePreviouslyOwnedLightWeapon(int weaponIndex)
    {
        this.previouslyOwnedLightWeapon = base.playerUpgradesState.LightGuns[weaponIndex];
    }
    public void UpdatePreviouslyOwnedHeavyWeapon(int weaponIndex)
    {
        this.previouslyOwnedLightWeapon = base.playerUpgradesState.HeavyGuns[weaponIndex];
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
