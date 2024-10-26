using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;


public class PurchaseItem : ItemStoreAction
{
    [SerializeField] private FloatReference playerMoney;
    public event Action SelectedCellPurchased;

    public void PurchaseCell(UpgradeCell cell)
    {
            if (!IsAbleToMakePurchase(cell)) 
                return;

            DeductFromPlayerMoney(cell.Cost());

            SaveToJson(cell);
            
            cell.IsOwned = true;
            this.SelectedCellPurchased?.Invoke();
        }


    private bool IsAbleToMakePurchase(UpgradeCell cell)
    {
        float PlayerMoneyFloat = (float)this.playerMoney.GetValue();
        bool unableToMakePurchase = cell.Cost() > PlayerMoneyFloat || !cell.Purchaseable();
        if (unableToMakePurchase)
        {
            return false;
        }

        return true;
    }

    private void DeductFromPlayerMoney(float amountToDeduct)
    {
        float PlayerMoneyFloat = (float)this.playerMoney.GetValue();
        this.playerMoney.SetValue(PlayerMoneyFloat - amountToDeduct);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.playerMoney.GetValue());
    }
    protected override void SaveToJson(UpgradeCell cell)
    {
        switch (cell.ShipSection)
        {
            case ShipSections.lightWeapons:
                base.SaveLightWeaponAction(cell);
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
