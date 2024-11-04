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
        this.playerWeaponsState.EditLightWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(this.playerWeaponsState.LightGunsAbstract);
        string jsonString = JsonUtility.ToJson(weapons, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.lightWeaponConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
    }

    private void SaveHeavyWeaponAction(UpgradeCell cell)
    {
        this.playerWeaponsState.EditHeavyWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(this.playerWeaponsState.HeavyGunsAbstract);
        string jsonString = JsonUtility.ToJson(weapons, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.heavyWeaponConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.heavyWeaponConfigurationSaveFile);
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
                break;
            case ShipSections.engine:
                break;
            case ShipSections.energy:
                break;
            default:
                break;
        }
    }

    public void UpdatePreviouslyOwnedLightWeapon(int weaponIndex)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.LightGuns[weaponIndex];
    }
    public void UpdatePreviouslyOwnedHeavyWeapon(int weaponIndex)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.HeavyGuns[weaponIndex];
    }

    public void UpdatePreviouslyOwnedArmour()
    {
        this.previouslyOwnedArmour = base.playerWeaponsState.Armour;
    }

    public void UpdatePreviouslyOwnedEnergySystem()
    {
        this.previouslyOwnedEnergySystem = base.playerWeaponsState.EnergySystem;
    }

    public void UpdatePreviouslyOwnedEngine()
    {
        this.previouslyOwnedEngine = base.playerWeaponsState.Engine;
    }
}
