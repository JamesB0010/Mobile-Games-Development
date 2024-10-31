using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerWeaponsState : ScriptableObject
{
    [Header("Light Guns")]
    [SerializeField] private LightGunUpgrade defaultLightGun;

    [SerializeField] private List<LightGunUpgrade> lightGuns;
    public List<LightGunUpgrade> LightGuns
    {
        get => this.lightGuns;

        set => this.lightGuns = value;
    }

    public List<ShipGunUpgrade> LightGunsAbstract => this.lightGuns.Cast<ShipGunUpgrade>().ToList();

    public void ResetLightGuns()
    {
        for (int i = 0; i < this.lightGuns.Count; i++)
        {
            this.lightGuns[i] = this.defaultLightGun;
        }
    }



    [Space(5)]
    [Header("Heavy Guns")]
    [SerializeField] private HeavyGunUpgrade defaultHeavyGun;

    [SerializeField] private List<HeavyGunUpgrade> heavyGuns;
    public List<HeavyGunUpgrade> HeavyGuns
    {
        get => this.heavyGuns;
        set => this.heavyGuns = value;
    }

    public List<ShipGunUpgrade> HeavyGunsAbstract => this.heavyGuns.Cast<ShipGunUpgrade>().ToList();

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
        this.lightGuns[index] = (LightGunUpgrade)gun;
    }

    public void EditHeavyWeaponAtIndex(int index, ShipGunUpgrade gun)
    {
        this.heavyGuns[index] = (HeavyGunUpgrade)gun;
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
