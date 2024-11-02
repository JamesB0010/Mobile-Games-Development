using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerWeaponsState : ScriptableObject
{
    [FormerlySerializedAs("defaultLightGun")]
    [Header("Light Guns")]
    [SerializeField] private LightItemUpgrade defaultLightItem;

    [SerializeField] private List<LightItemUpgrade> lightGuns;
    public List<LightItemUpgrade> LightGuns
    {
        get => this.lightGuns;

        set => this.lightGuns = value;
    }

    public List<ShipItemUpgrade> LightGunsAbstract => this.lightGuns.Cast<ShipItemUpgrade>().ToList();

    public void ResetLightGuns()
    {
        for (int i = 0; i < this.lightGuns.Count; i++)
        {
            this.lightGuns[i] = this.defaultLightItem;
        }
    }



    [FormerlySerializedAs("defaultHeavyGun")]
    [Space(5)]
    [Header("Heavy Guns")]
    [SerializeField] private HeavyItemUpgrade defaultHeavyItem;

    [SerializeField] private List<HeavyItemUpgrade> heavyGuns;
    public List<HeavyItemUpgrade> HeavyGuns
    {
        get => this.heavyGuns;
        set => this.heavyGuns = value;
    }

    public List<ShipItemUpgrade> HeavyGunsAbstract => this.heavyGuns.Cast<ShipItemUpgrade>().ToList();

    public void ResetHeavyGuns()
    {
        for (int i = 0; i < this.heavyGuns.Count; i++)
        {
            this.heavyGuns[i] = this.defaultHeavyItem;
        }
    }

    [Space(5)]
    [Header("Shield / Armour")]
    [SerializeField] private ShipItemUpgrade shield;

    public ShipItemUpgrade Shield
    {
        get => this.shield;
        set => this.shield = value;
    }

    [Space(5)]
    [Header("Engine / Booster")]
    [SerializeField] private ShipItemUpgrade engine;

    public ShipItemUpgrade Engine
    {
        get => this.engine;
        set => this.engine = value;
    }

    public void EditLightWeaponAtIndex(int index, ShipItemUpgrade item)
    {
        this.lightGuns[index] = (LightItemUpgrade)item;
    }

    public void EditHeavyWeaponAtIndex(int index, ShipItemUpgrade item)
    {
        this.heavyGuns[index] = (HeavyItemUpgrade)item;
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
