using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimePurcaseAcievementIDCollection : ScriptableObject
{
    [SerializeField] private string lightWeapon;
    public string LigtWeapon => this.lightWeapon;

    [SerializeField] private string heavyWeapon;
    public string HeavyWeapon => this.heavyWeapon;

    [SerializeField] private string armour;
    public string Armour => this.armour;

    [SerializeField] private string energySystem;
    public string EnergySystem => this.energySystem;

    [SerializeField] private string engine;
    public string Engine => this.engine;
}
