using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIViewUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameField, itemFireRateField, damagePerShotField, costField, purchaseEquipButtonText;

    [SerializeField] private PlayerWeaponsState playerWeaponsState;
    public void UpdateText( int side)
    {
        Gun gun = this.playerWeaponsState.Guns[side];
        itemNameField.text = gun.name;

        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Cost().ToString();
        this.purchaseEquipButtonText.text = selectedCell.IsOwned == false ? "Purchase" : "Equip";
    }

    public void SetPurchaseButtonText(string newText)
    {
        this.purchaseEquipButtonText.text = newText;
    }
}
