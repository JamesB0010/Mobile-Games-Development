using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

[System.Serializable]
public class PlayerWeaponsState : ScriptableObject
{
    [SerializeField] private List<Gun> lightGuns;

    public List<Gun> LightGuns
    {
        get => this.lightGuns;
        
        set => this.lightGuns = value;
    }


    [SerializeField] private List<Gun> heavyGuns;

    public List<Gun> HeavyGuns
    {
        get => this.heavyGuns;
        set => this.heavyGuns = value;
    }

    [SerializeField] private Gun shield;

    public Gun Shield
    {
        get => this.shield;
        set => this.shield = value;
    }

    [SerializeField] private Gun engine;

    public Gun Engine
    {
        get => this.engine;
        set => this.engine = value;
    }

    public void EditWeaponAtIndex(int index, Gun gun)
    {
        this.lightGuns[index] = gun;
    }

    public void SetPlayershipWithStoredWeapons(PlayerShipWeapon[] weapons)
    {
        for (int i = 0; i < this.lightGuns.Count; i++)
        {
            weapons[i].Gun = (Gun)this.lightGuns[i].Clone();
        }
    }
}
