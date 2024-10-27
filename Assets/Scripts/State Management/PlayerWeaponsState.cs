using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerWeaponsState : ScriptableObject
{
    [SerializeField] private List<ShipGunUpgrade> lightGuns;

    [SerializeField] private ShipGunUpgrade defaultLightGun;
    public List<ShipGunUpgrade> LightGuns
    {
        get => this.lightGuns;
        
        set => this.lightGuns = value;
    }

    public void ResetLightGuns()
    {
        for (int i = 0; i < this.lightGuns.Count; i++)
        {
            this.lightGuns[i] = this.defaultLightGun;
        }
    }


    [SerializeField] private List<ShipGunUpgrade> heavyGuns;

    public List<ShipGunUpgrade> HeavyGuns
    {
        get => this.heavyGuns;
        set => this.heavyGuns = value;
    }

    [SerializeField] private ShipGunUpgrade shield;

    public ShipGunUpgrade Shield
    {
        get => this.shield;
        set => this.shield = value;
    }

    [SerializeField] private ShipGunUpgrade engine;

    public ShipGunUpgrade Engine
    {
        get => this.engine;
        set => this.engine = value;
    }

    public void EditWeaponAtIndex(int index, ShipGunUpgrade gun)
    {
        this.lightGuns[index] = gun;
    }

    public void SetPlayershipWithStoredWeapons(PlayerShipWeapon[] weapons)
    {
        for (int i = 0; i < this.lightGuns.Count; i++)
        {
            weapons[i].Gun = (Gun)this.lightGuns[i].Gun.Clone();
        }
    }
}
