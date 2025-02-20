using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerUpgradesState : ScriptableObject
{
    public event Action<float> shipFirepowerChanged;
    public event Action<float> shipArmourChanged;

    public event Action<float> shipEnergySystemChanged;
    
    public event Action<float> shipMaxSpeedChanged;
    
    
    [FormerlySerializedAs("defaultLightItem")]
    [Header("Light Guns")]
    [SerializeField] private LightGunUpgrade defaultLightGun;

    public LightGunUpgrade DefaultLightGun => this.defaultLightGun;

    [SerializeField] private LightGunUpgrade noLightGun;

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
            if (i < 2)
            {
                this.lightGuns[i] = this.defaultLightGun;
                continue;
            }

            this.lightGuns[i] = noLightGun;
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

    public void ResetAll()
    {
        this.ResetArmour();
        this.ResetEngine();
        this.ResetEnergySystem();
        this.ResetLightGuns();
        this.ResetHeavyGuns();
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
        
        this.shipFirepowerChanged?.Invoke(this.CalculateShipFirepower());
    }

    public void EditHeavyWeaponAtIndex(int index, ShipItemUpgrade item)
    {
        this.heavyGuns[index] = (HeavyGunUpgrade)item;
        
        this.shipFirepowerChanged?.Invoke(this.CalculateShipFirepower());
    }

    public float CalculateShipFirepower()
    {
        int firepower = 0;
        this.lightGuns.ForEach(upgrade =>
        {
            firepower += upgrade.Gun.Firepower;
        });
        
        this.heavyGuns.ForEach(upgrade =>
        {
            firepower += upgrade.Gun.Firepower;
        });

        return firepower;
    }

    public void EditEngine(EngineUpgrade engine)
    {
        this.engine = engine;
        
        this.shipMaxSpeedChanged?.Invoke(this.engine.Engine.MaxVelocity);
    }

    public void EditEnergySystem(EnergySystemsUpgrade energySystem)
    {
        this.energySystem = energySystem;
        
        this.shipEnergySystemChanged?.Invoke(this.energySystem.EnergySystem.MaxEnergy);
    }

    public void EditArmour(ArmourUpgrade armour)
    {
        this.armour = armour;
        
        this.shipArmourChanged?.Invoke(armour.Armour.DamageDampeningMultiplier);
    }

    public void SetPlayershipWithStoredLightWeapons(PlayerShipLightWeapon[] weapons)
    {
        for (int i = 0; i < this.lightGuns.Count; i++)
        {
            if (this.lightGuns[i] == noLightGun)
            {
                weapons[i].gameObject.SetActive(false);
                continue;
            }
       
            weapons[i].LightGun = (LightGun)this.lightGuns[i].Gun;
            weapons[i].SetupWeapon();
        }
    }

    public void SetPlayershipWithStoredHeavyWeapons(PlayerShipHeavyWeapon[] weapons)
    {
        for (int i = 0; i < this.HeavyGuns.Count; i++)
        {
            if (this.HeavyGuns[i] == this.defaultHeavyGun)
            {
                weapons[i].gameObject.SetActive(false);
                continue;
            }

            weapons[i].HeavyGun = (HeavyGun)this.heavyGuns[i].Gun;
            weapons[i].SetupWeapon();
            weapons[i].HeavyGun.CurrentAmmoCount = weapons[i].HeavyGun.MaxAmmoCount;
        }
    }

    public void SetupComplete()
    {
        this.shipFirepowerChanged?.Invoke(this.CalculateShipFirepower());
        this.shipArmourChanged?.Invoke(this.armour.Armour.DamageDampeningMultiplier);
        this.shipEnergySystemChanged?.Invoke(this.energySystem.EnergySystem.MaxEnergy);
        this.shipMaxSpeedChanged?.Invoke(this.engine.Engine.MaxVelocity);
    }
}
