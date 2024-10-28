using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIViewUpdater : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemNameField,
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
                LightGun lightLightGun = this.playerWeaponsState.LightGuns[side].LightGun;
                this.SetItemStatsUi(lightLightGun);
                break;
            case ShipSections.heavyWeapons:
                LightGun heavyLightGun = this.playerWeaponsState.HeavyGuns[side].LightGun;
                this.SetItemStatsUi(heavyLightGun);
                break;
            case ShipSections.armour:
                LightGun armour = this.playerWeaponsState.Shield.LightGun;
                this.SetItemStatsUi(armour);
                break;
            case ShipSections.engine:
                LightGun engine = this.playerWeaponsState.Shield.LightGun;
                this.SetItemStatsUi(engine);
                break;
            default:
                break;
        }

    }

    private void SetItemStatsUi(LightGun lightLightGun)
    {
        itemNameField.text = lightLightGun.name;

        this.itemFireRateField.text = lightLightGun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = lightLightGun.BulletDamage.ToString();
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Upgrade.Cost.ToString();
        if (selectedCell.Upgrade.IsPurchaseable)
        {
            bool isEquipped = playerWeaponsState.LightGuns[selectedCell.WeaponIndex] == selectedCell.Upgrade;
            bool isOwned = OwnedUpgradesCounter.Instance.GetUpgradeCount(selectedCell.Upgrade) > 0 || selectedCell.Upgrade.OwnedByDefault || isEquipped;
            if (isOwned)
            {
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
        LightGun lightGun = cell.Upgrade.LightGun;
        itemNameField.text = lightGun.name;

        this.itemFireRateField.text = lightGun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = lightGun.BulletDamage.ToString();
    }
}
