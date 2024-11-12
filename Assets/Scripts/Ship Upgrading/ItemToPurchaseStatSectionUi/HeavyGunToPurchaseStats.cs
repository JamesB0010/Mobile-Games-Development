using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeavyGunToPurchaseStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noGunHeader;
    [SerializeField]
    private TextMeshProUGUI weaponNameValField, fireRateValField, bulletDamageValField, ammoCountValField;

    public void SetFields(string weaponName, string fireRate, string bulletDamage, string ammoCount)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>().enabled = true;
        }

        this.noGunHeader.enabled = false;
        
        this.weaponNameValField.text = weaponName;
        this.fireRateValField.text = fireRate;
        this.bulletDamageValField.text = bulletDamage;
        this.ammoCountValField.text = ammoCount;
    }

    public void WeaponCantShoot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>().enabled = false;
        }
        
        this.noGunHeader.enabled = true;
    }
}
