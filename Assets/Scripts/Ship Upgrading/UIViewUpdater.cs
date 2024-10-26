using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIViewUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameField,
        itemFireRateField,
        damagePerShotField,
        costField,
        purchaseEquipButtonText,
        playerMoneyField;

    [SerializeField] private PlayerWeaponsState playerWeaponsState;

    [SerializeField] private FloatReference playerMoney;

    private void Start()
    {
        this.playerMoneyField.text = ((float)playerMoney.GetValue()).ToString();
    }

    public void UpdateItemDetailsText( int side)
    {
        Gun gun = this.playerWeaponsState.Guns[side];
        itemNameField.text = gun.name;

        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Upgrade.Cost.ToString();
        this.purchaseEquipButtonText.text = selectedCell.Upgrade.Gun.OwnedByPlayer == false ? "Purchase" : "Equip";
    }

    public void CellPurchased(SelectedCellHighlight highlight)
    {
        this.purchaseEquipButtonText.text = "Equip";
        this.playerMoneyField.text = ((float)this.playerMoney.GetValue()).ToString();



        this.UpdateUiBasedOnGun(highlight.SelectedCell);
        
    }


    public void CellEquipped(SelectedCellHighlight highlight)
    {
        this.UpdateUiBasedOnGun(highlight.SelectedCell);
    }
    private void UpdateUiBasedOnGun(UpgradeCell cell)
    {
        Gun gun = cell.Upgrade.Gun;
        itemNameField.text = gun.name;
                
        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();
                
        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }
}
