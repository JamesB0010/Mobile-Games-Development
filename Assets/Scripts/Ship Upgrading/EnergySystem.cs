using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Energy System")]
public class EnergySystem : ShipItem
{
    public event Action<float> CurrentEnergyChanged;

    public event Action<float> CurrentEnergyPeaked;

    private float currentEnergy;
    public float CurrentEnergy
    {
        get => this.currentEnergy;
        set
        {
            if (value >= this.maxEnergy)
            {
                this.currentEnergy = this.maxEnergy;
                this.CurrentEnergyPeaked?.Invoke(this.maxEnergy);
                return;
            }

            this.currentEnergy = value;
            
            if (this.currentEnergy < 0)
                this.currentEnergy = 0;
            
            this.CurrentEnergyChanged?.Invoke(value);
        }
    }

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

    public void Update()
    {
        this.CurrentEnergy += this.rechargeRate * Time.deltaTime;
    }
}
