using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EquipItem : ItemShopAction
{
    public event Action SelectedCellEquipped;

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
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "lightWeaponConfiguration.txt"), jsonString);
        BuzzardGameData.ReloadTextFiles();
    }

    public void SaveHeavyWeaponAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditHeavyWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedUpgradesJsonObject upgrades = new SavedUpgradesJsonObject(this.playerUpgradesState.HeavyGunsAbstract);
        string jsonString = JsonUtility.ToJson(upgrades, true);
        string filepath = Path.Combine(Application.persistentDataPath, "Json", "heavyWeaponConfiguration.txt");
        File.WriteAllText(filepath, jsonString);
        BuzzardGameData.ReloadTextFiles();
    }

    public void SaveEngineAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditEngine((EngineUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.EngineAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath + "Json", "engineConfiguration.txt") ,jsonString);
        BuzzardGameData.ReloadTextFiles();
    }

    public void SaveEnergySystemAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditEnergySystem((EnergySystemsUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.EnergySystemAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "energySystemConfiguration.txt"), jsonString);
        BuzzardGameData.ReloadTextFiles();
    }

    public void SaveArmourAction(UpgradeCell cell)
    {
        this.playerUpgradesState.EditArmour((ArmourUpgrade)cell.Upgrade);
        SavedUpgradesJsonObject upgrade = new SavedUpgradesJsonObject(this.playerUpgradesState.ArmourAbstract);
        string jsonString = JsonUtility.ToJson(upgrade, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "armourConfiguration.txt"), jsonString);
        BuzzardGameData.ReloadTextFiles();
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
