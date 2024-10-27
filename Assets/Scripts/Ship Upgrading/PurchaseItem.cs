using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;


public class PurchaseItem : ItemShopAction
{
    [SerializeField] private FloatReference playerMoney;
    public event Action SelectedCellPurchased;

    public void PurchaseCell(UpgradeCell cell)
    {
            if (!IsAbleToMakePurchase(cell)) 
                return;

            DeductFromPlayerMoney(cell.Upgrade.Cost);

            SaveToJson(cell);
            
            this.SelectedCellPurchased?.Invoke();
        }


    private bool IsAbleToMakePurchase(UpgradeCell cell)
    {
        float PlayerMoneyFloat = (float)this.playerMoney.GetValue();
        bool unableToMakePurchase = cell.Upgrade.Cost > PlayerMoneyFloat || !cell.Upgrade.IsPurchaseable;
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
