using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponStatsDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameField, itemFireRateField, damagePerShotField;

    public void UpdateText(PlayerWeaponsState playerWeaponsState, int side)
    {
        Gun gun = playerWeaponsState.Guns[side];
        itemNameField.text = gun.name;

        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }
}
