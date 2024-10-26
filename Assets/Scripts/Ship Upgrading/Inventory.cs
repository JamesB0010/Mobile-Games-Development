using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;


//Responsibilities
//Setting the purchase button to say purchase or equip depending on if the item is already owned
public class Inventory : MonoBehaviour
{
    [SerializeField] private RectTransform highlightImage;
    [SerializeField] private TextMeshProUGUI costUiField;
    [SerializeField] private PlayerWeaponsState playerWeaponsState;
    [SerializeField] private TextMeshProUGUI playerBalanceUiText;
    [SerializeField] private FloatReference playerMoney;
    [SerializeField] private TextMeshProUGUI EquippedGunNameUiField;
    [SerializeField] private TextAsset lightWeaponConfigurationSaveFile;
    [SerializeField] private TextMeshProUGUI purchaseButtonText;

    private UpgradeSceneManager upgradeSceneManager;
    private void Start()
    {
        this.playerBalanceUiText.text = ((float)playerMoney.GetValue()).ToString();
        this.upgradeSceneManager = FindObjectOfType<UpgradeSceneManager>();
    }
    public void PurchaseSelectedCell(SelectedCellHighlight highlight)
    {
        UpgradeCell cell = highlight.SelectedCell;
        
        if (cell.IsOwned && cell.GunOwnedByThisSide())
        {
            Debug.Log("Equip item");
            EquippedGunNameUiField.text = cell.UpgradeName();
            this.playerWeaponsState.EditWeaponAtIndex(cell.WeaponIndex,
                                    cell.GetGun());
                                SavedLightWeaponsJsonObject lightWeapons =
                                    new SavedLightWeaponsJsonObject(this.playerWeaponsState.Guns);
                                string jsonString = JsonUtility.ToJson(lightWeapons);
                                File.WriteAllText(Application.dataPath + "/Json/lightWeaponConfiguration.txt", jsonString);
                                AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
        }
        else
        {
            float PlayerMoneyFloat = (float)this.playerMoney.GetValue();
            if (cell.Cost() > PlayerMoneyFloat || !cell.Purchaseable())
            {
                return;
            }

            //Do saving weapon logic here
            this.playerMoney.SetValue(PlayerMoneyFloat - cell.Cost());
            PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.playerMoney.GetValue());
            this.playerBalanceUiText.text = ((float)playerMoney.GetValue()).ToString();
            EquippedGunNameUiField.text = cell.UpgradeName();
            cell.IsOwned = true;
            Debug.Log("Purchased: " + cell.UpgradeName());
            this.upgradeSceneManager.CellPurchased();

            //Save to Json
            switch (cell.ShipSection)
            {
                case ShipSections.lightWeapons:
                    this.playerWeaponsState.EditWeaponAtIndex(cell.WeaponIndex,
                        cell.GetGun());
                    SavedLightWeaponsJsonObject lightWeapons =
                        new SavedLightWeaponsJsonObject(this.playerWeaponsState.Guns);
                    string jsonString = JsonUtility.ToJson(lightWeapons);
                    File.WriteAllText(Application.dataPath + "/Json/lightWeaponConfiguration.txt", jsonString);
                    AssetDatabase.SaveAssetIfDirty(this.lightWeaponConfigurationSaveFile);
                    cell.AddThisSideToUpgradeOwnedSides();
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
    }
}
