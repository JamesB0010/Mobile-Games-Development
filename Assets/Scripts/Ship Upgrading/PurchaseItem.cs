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
        //PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.playerMoney.GetValue());
        //todo replace with save game approach
    }
}
