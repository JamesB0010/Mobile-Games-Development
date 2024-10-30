using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerWeaponsState : ScriptableObject
{
    [Header("Light Guns")]
    [SerializeField] private ShipGunUpgrade defaultLightGun;

    [SerializeField] private List<ShipGunUpgrade> lightGuns;
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



    [Space(5)]
    [Header("Heavy Guns")]
    [SerializeField] private ShipGunUpgrade defaultHeavyGun;

    [SerializeField] private List<ShipGunUpgrade> heavyGuns;
    public List<ShipGunUpgrade> HeavyGuns
    {
        get => this.heavyGuns;
        set => this.heavyGuns = value;
    }

    public void ResetHeavyGuns()
    {
        for (int i = 0; i < this.heavyGuns.Count; i++)
        {
            this.heavyGuns[i] = this.defaultHeavyGun;
        }
    }

    [Space(5)]
    [Header("Shield / Armour")]
    [SerializeField] private ShipGunUpgrade shield;

    public ShipGunUpgrade Shield
    {
        get => this.shield;
        set => this.shield = value;
    }

    [Space(5)]
    [Header("Engine / Booster")]
    [SerializeField] private ShipGunUpgrade engine;

    public ShipGunUpgrade Engine
    {
        get => this.engine;
        set => this.engine = value;
    }

    public void EditLightWeaponAtIndex(int index, ShipGunUpgrade gun)
    {
        this.lightGuns[index] = gun;
    }

    public void EditHeavyWeaponAtIndex(int index, ShipGunUpgrade gun)
    {
        this.heavyGuns[index] = gun;
    }

    public void SetPlayershipWithStoredLightWeapons(PlayerShipLightWeapon[] weapons)
    {
        int i = 0;
        for (; i < this.lightGuns.Count; i++)
        {
            if(i >= weapons.Length)
                break;
            weapons[i].LightGun = (LightGun)this.lightGuns[i].Gun.Clone();
        }

        for (; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }

    public void SetPlayershipWithStoredHeavyWeapons(PlayerShipHeavyWeapon[] weapons)
    {
        int i = 0;
        for (; i < this.heavyGuns.Count; i++)
        {
            if(i >= weapons.Length)
                break;
            weapons[i].HeavyGun = (HeavyGun)this.heavyGuns[i].Gun.Clone();
        }

        for (; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }
}
