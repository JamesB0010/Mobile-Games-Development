using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerWeaponsState : ScriptableObject
{
    [FormerlySerializedAs("defaultLightItem")]
    [Header("Light Guns")]
    [SerializeField] private LightGunUpgrade defaultLightGun;

    [SerializeField] private List<LightGunUpgrade> lightGuns;
    public List<LightGunUpgrade> LightGuns
    {
        get => this.lightGuns;

        set => this.lightGuns = value;
    }

    public List<ShipItemUpgrade> LightGunsAbstract => this.lightGuns.ConvertAll(child => (ShipItemUpgrade)child);

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

    public List<ShipItemUpgrade> HeavyGunsAbstract => this.heavyGuns.ConvertAll(child => (ShipItemUpgrade)child);

    public void ResetHeavyGuns()
    {
        for (int i = 0; i < this.heavyGuns.Count; i++)
        {
            this.heavyGuns[i] = this.defaultHeavyGun;
        }
    }

    [FormerlySerializedAs("shield")]
    [Space(5)]
    [Header("Shield / Armour")]
    [SerializeField] private ArmourUpgrade armour;

    [SerializeField] private ArmourUpgrade defaultArmour;

    public ArmourUpgrade Armour
    {
        get => this.armour;
        set => this.armour = value;
    }

    public ShipItemUpgrade ArmourAbstract => this.armour;

    public void ResetArmour()
    {
        this.armour = this.defaultArmour;
    }

    [Space(5)]
    [Header("Engine / Booster")]
    [SerializeField] private EngineUpgrade engine;

    [SerializeField] private EngineUpgrade defaultEngine;

    public EngineUpgrade Engine
    {
        get => this.engine;
        set => this.engine = value;
    }

    public ShipItemUpgrade EngineAbstract => this.engine;

    public void ResetEngine()
    {
        this.engine = this.defaultEngine;
    }

    [Space(5)]
    [Header("Energy System")]
    [SerializeField]
    private EnergySystemsUpgrade energySystem;

    [SerializeField] private EnergySystemsUpgrade defaultEnergySystem;

    public EnergySystemsUpgrade EnergySystem
    {
        get => this.energySystem;
        set => this.energySystem = value;
    }

    public ShipItemUpgrade EnergySystemAbstract => this.energySystem;

    public void ResetEnergySystem()
    {
        this.energySystem = this.defaultEnergySystem;
    }

    public void EditLightWeaponAtIndex(int index, ShipItemUpgrade item)
    {
        this.lightGuns[index] = (LightGunUpgrade)item;
    }

    public void EditHeavyWeaponAtIndex(int index, ShipItemUpgrade item)
    {
        this.heavyGuns[index] = (HeavyGunUpgrade)item;
    }

    public void SetPlayershipWithStoredLightWeapons(PlayerShipLightWeapon[] weapons)
    {
        int i = 0;
        for (; i < this.lightGuns.Count; i++)
        {
            if (i >= weapons.Length)
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
            if (i >= weapons.Length)
                break;
            weapons[i].HeavyGun = (HeavyGun)this.heavyGuns[i].Gun.Clone();
        }

        for (; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }
}
