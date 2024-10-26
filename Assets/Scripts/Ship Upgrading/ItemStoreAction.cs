using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public abstract class ItemStoreAction : ScriptableObject
{
    [SerializeField] protected PlayerWeaponsState playerWeaponsState;
    [SerializeField] protected TextAsset lightWeaponConfigurationSaveFile;

    protected void SaveLightWeaponAction(UpgradeCell cell)
    {
        this.playerWeaponsState.EditWeaponAtIndex(cell.WeaponIndex, cell.Upgrade.Gun);
        SavedLightWeaponsJsonObject lightWeapons = new SavedLightWeaponsJsonObject(this.playerWeaponsState.Guns);
        string jsonString = JsonUtility.ToJson(lightWeapons);
        File.WriteAllText(Application.dataPath + "/Json/lightWeaponConfiguration.txt", jsonString);
        AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
    }

    protected abstract void SaveToJson(UpgradeCell cell);
}
