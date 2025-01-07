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
    }
}
