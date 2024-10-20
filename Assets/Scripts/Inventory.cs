using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private UpgradeCell selectedCell;
    [SerializeField] private RectTransform highlightImage;
    [SerializeField] private TextMeshProUGUI costUiField;
    [SerializeField] private PlayerWeaponsState playerWeaponsState;
    [SerializeField] private TextMeshProUGUI playerBalanceUiText;
    [SerializeField] private FloatReference playerMoney;
    [SerializeField] private TextMeshProUGUI EquippedGunNameUiField;
    private void Start()
    {
        this.playerMoney.SetValue(PlayerPrefs.GetFloat(PlayerPrefsKeys.PlayerMoneyKey));
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
        this.playerWeaponsState.EditWeaponAtIndex(this.selectedCell.WeaponIndex, this.selectedCell.GetGun());
        EquippedGunNameUiField.text = this.selectedCell.UpgradeName();
        Debug.Log("Purchased: " + selectedCell.UpgradeName());
    }
    public void SelectCell(UpgradeCell cell)
    {
        this.highlightImage.position = cell.transform.position;
        this.selectedCell = cell;
        this.costUiField.text = cell.Cost().ToString();
    }
}
