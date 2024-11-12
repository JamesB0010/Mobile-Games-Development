using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightGunToPurchaseStats : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI weaponNameValField, fireRateValField, bulletDamageValField, energyExpenseValField;

    public void SetFields(string weaponName, string fireRate, string bulletDamage, string energyExpense)
    {
        this.weaponNameValField.text = weaponName;
        this.fireRateValField.text = fireRate;
        this.bulletDamageValField.text = bulletDamage;
        this.energyExpenseValField.text = energyExpense;
    }
}
