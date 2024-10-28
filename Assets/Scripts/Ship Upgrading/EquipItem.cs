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


    private ShipGunUpgrade previouslyOwnedLightWeapon;
    public ShipGunUpgrade PreviouslyOwnedLightWeapon => this.previouslyOwnedLightWeapon;

    private ShipGunUpgrade previouslyOwnedHeavyWeapon;

    public ShipGunUpgrade PreviouslyOwnedHeavyWeapon => this.previouslyOwnedHeavyWeapon;

    public void EquipCell(UpgradeCell cell)
    {
        SaveToJson(cell);
    }
    private void SaveLightWeaponAction(UpgradeCell cell)
    {
        this.playerWeaponsState.EditLightWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(this.playerWeaponsState.LightGuns);
        string jsonString = JsonUtility.ToJson(weapons, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.lightWeaponConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
    }

    private void SaveHeavyWeaponAction(UpgradeCell cell)
    {
        this.playerWeaponsState.EditHeavyWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedWeaponsJsonObject weapons = new SavedWeaponsJsonObject(this.playerWeaponsState.HeavyGuns);
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
            default:
                break;
        }
    }
    public void UpdatePreviouslyOwnedLightWeapon(ShipPartLabel label)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.LightGuns[label.WeaponIndex];
    }


    public void UpdatePreviouslyOwnedLightWeapon(int side)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.LightGuns[side];
    }
    public void UpdatePreviouslyOwnedHeavyWeapon(int side)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.HeavyGuns[side];
    }
    public void UpdatePreviouslyOwnedHeavyWeapon(ShipPartLabel label)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.HeavyGuns[label.WeaponIndex];
    }
}
