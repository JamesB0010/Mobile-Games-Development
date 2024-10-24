using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

[Serializable]
public enum ShipSections
{
    lightWeapons,
    heavyWeapons,
    armour,
    engine
}
public class Inventory : MonoBehaviour
{
    private UpgradeCell selectedCell;
    [SerializeField] private RectTransform highlightImage;
    [SerializeField] private TextMeshProUGUI costUiField;
    [SerializeField] private PlayerWeaponsState playerWeaponsState;
    [SerializeField] private TextMeshProUGUI playerBalanceUiText;
    [SerializeField] private FloatReference playerMoney;
    [SerializeField] private TextMeshProUGUI EquippedGunNameUiField;
    [SerializeField] private TextAsset lightWeaponConfigurationSaveFile;
    private void Start()
    {
        this.playerBalanceUiText.text = ((float)playerMoney.GetValue()).ToString();
    }
    public void PurchaseSelectedCell()
    {
        float PlayerMoneyFloat = (float)this.playerMoney.GetValue();
        if (this.selectedCell.Cost() > PlayerMoneyFloat || !this.selectedCell.Purchaseable())
        {
            return;
        }
        //Do saving weapon logic here
        this.playerMoney.SetValue(PlayerMoneyFloat - this.selectedCell.Cost());
        PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.playerMoney.GetValue());
        this.playerBalanceUiText.text = ((float)playerMoney.GetValue()).ToString();
        EquippedGunNameUiField.text = this.selectedCell.UpgradeName();
        Debug.Log("Purchased: " + selectedCell.UpgradeName());
        
        
        //Save to Json
        switch (this.selectedCell.ShipSection)
        {
            case ShipSections.lightWeapons:
                this.playerWeaponsState.EditWeaponAtIndex(this.selectedCell.WeaponIndex, this.selectedCell.GetGun());
                SavedLightWeaponsJsonObject lightWeapons = new SavedLightWeaponsJsonObject(this.playerWeaponsState.Guns);
                string jsonString = JsonUtility.ToJson(lightWeapons);
                File.WriteAllText(Application.dataPath + "/Json/lightWeaponConfiguration.txt", jsonString);
                AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
                break;
            case ShipSections.heavyWeapons:
                break;
            case ShipSections.armour:
                break;
            case ShipSections.engine:
                break;
            default:
                break;
        }
    }
    public void SelectCell(UpgradeCell cell)
    {
        this.highlightImage.position = cell.transform.position;
        this.selectedCell = cell;
        this.costUiField.text = cell.Cost().ToString();
    }
}
