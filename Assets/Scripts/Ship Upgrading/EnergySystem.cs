using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Energy System")]
public class EnergySystem : ShipItem
{
    public float CurrentEnergy { get; set; }
    [SerializeField] private float maxEnergy;

    [SerializeField] private float rechargeRate;

    public float MaxEnergy => this.maxEnergy;

    public float RechargeRate => this.rechargeRate;
    
    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }

    public void ResetCurrentEnergy()
    {
        this.CurrentEnergy = this.maxEnergy;
    }
}
