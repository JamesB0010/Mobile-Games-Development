using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Energy System")]
public class EnergySystem : ShipItem
{
    public event Action<float> CurrentEnergyChanged;

    public event Action<float> CurrentEnergyPeaked;

    private float currentEngine;
    public float CurrentEnergy
    {
        get => this.currentEngine;
        set
        {
            if (value >= this.maxEnergy)
            {
                this.currentEngine = this.maxEnergy;
                this.CurrentEnergyPeaked?.Invoke(this.maxEnergy);
                return;
            }

            this.currentEngine = value;
            
            if (this.currentEngine < 0)
                this.currentEngine = 0;
            
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
