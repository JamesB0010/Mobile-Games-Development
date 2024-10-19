using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsState : ScriptableObject
{
    [SerializeField] private Gun[] guns;

    public void EditWeaponAtIndex(int index, Gun gun)
    {
        this.guns[index] = gun;
    }

    public void SetCorrectWeapons(PlayerShipWeapon[] weapons)
    {
        for (int i = 0; i < this.guns.Length; i++)
        {
            weapons[i].Gun = this.guns[i];
        }
    }
}
