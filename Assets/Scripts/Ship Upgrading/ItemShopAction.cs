using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public abstract class ItemShopAction : ScriptableObject
{
    [SerializeField] protected PlayerWeaponsState playerWeaponsState;
    [SerializeField] protected TextAsset lightWeaponConfigurationSaveFile;

    protected void SaveLightWeaponAction(UpgradeCell cell)
    {
        this.playerWeaponsState.EditWeaponAtIndex(cell.WeaponIndex, cell.Upgrade);
        SavedLightWeaponsJsonObject lightWeapons = new SavedLightWeaponsJsonObject(this.playerWeaponsState.LightGuns);
        string jsonString = JsonUtility.ToJson(lightWeapons, true);
        File.WriteAllText(Application.dataPath + AssetDatabase.GetAssetPath(this.lightWeaponConfigurationSaveFile).Substring(6), jsonString);
        AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
    }

    protected abstract void SaveToJson(UpgradeCell cell);
}
