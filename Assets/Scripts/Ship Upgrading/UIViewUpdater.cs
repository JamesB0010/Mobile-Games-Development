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

    public void UpdateItemDetailsText(ShipSections shipSection, int side = 0)
    {
        switch (shipSection)
        {
            case ShipSections.lightWeapons:
                Gun lightGun = this.playerWeaponsState.LightGuns[side].Gun;
                this.SetItemStatsUi(lightGun);
                break;
            case ShipSections.heavyWeapons:
                Gun heavyGun = this.playerWeaponsState.HeavyGuns[side].Gun;
                this.SetItemStatsUi(heavyGun);
                break;
            case ShipSections.armour:
                Gun armour = this.playerWeaponsState.Shield.Gun;
                this.SetItemStatsUi(armour);
                break;
            case ShipSections.engine:
                Gun engine = this.playerWeaponsState.Shield.Gun;
                this.SetItemStatsUi(engine);
                break;
            default:
                break;
        }
        
    }

    private void SetItemStatsUi(Gun lightGun)
    {
        itemNameField.text = lightGun.name;

        this.itemFireRateField.text = lightGun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = lightGun.BulletDamage.ToString();
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Upgrade.Cost.ToString();
        if (selectedCell.Upgrade.IsPurchaseable)
        {
            bool isOwned = OwnedUpgradesCounter.Instance.GetUpgradeCount(selectedCell.Upgrade) <= 0;
            if (isOwned)
            {
                bool isEquipped = playerWeaponsState.LightGuns[selectedCell.WeaponIndex] == selectedCell.Upgrade;
                if (isEquipped)
                    this.purchaseEquipButtonText.text = "Equipped";
                else
                    this.purchaseEquipButtonText.text = "Equip";
            }
            else
                this.purchaseEquipButtonText.text = "Purchase";
        }
    }

    public void CellPurchased(SelectedCellHighlight highlight)
    {
        this.purchaseEquipButtonText.text = "Equipped";
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
