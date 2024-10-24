using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsState : ScriptableObject
{
    [SerializeField] private List<Gun> guns;

    public List<Gun> Guns
    {
        get => this.guns;
        
        set
        {
            this.guns = value;
        }
    }

    public void EditWeaponAtIndex(int index, Gun gun)
    {
        this.guns[index] = gun;
    }

    public void SetPlayershipWithStoredWeapons(PlayerShipWeapon[] weapons)
    {
        for (int i = 0; i < this.guns.Count; i++)
        {
            weapons[i].Gun = (Gun)this.guns[i].Clone();
        }
    }
}
