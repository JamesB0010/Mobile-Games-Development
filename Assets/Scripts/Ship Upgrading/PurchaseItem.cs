using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;


public class PurchaseItem : ItemShopAction
{
    public event Action SelectedCellPurchased;

    public event Action PurchaseFailed;

    public bool PurchaseCell(UpgradeCell cell)
    {
        if (!IsAbleToMakePurchase(cell))
        {
            this.PurchaseFailed?.Invoke();
            return false;
        }

        DeductFromPlayerMoney(cell.Upgrade.Cost);

        this.SelectedCellPurchased?.Invoke();
        return true;
    }


    private bool IsAbleToMakePurchase(UpgradeCell cell)
    {
        float PlayerMoneyFloat = BuzzardGameData.PlayerMoney.GetValue();
        bool unableToMakePurchase = cell.Upgrade.Cost > PlayerMoneyFloat || !cell.Upgrade.IsPurchaseable;
        if (unableToMakePurchase)
        {
            return false;
        }

        return true;
    }

    private void DeductFromPlayerMoney(float amountToDeduct)
    {
        FloatReference playerMoney = BuzzardGameData.PlayerMoney;
        float PlayerMoneyFloat = playerMoney.GetValue();
        playerMoney.SetValue(PlayerMoneyFloat - amountToDeduct);
    }
}
