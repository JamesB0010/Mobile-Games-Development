using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerShipEnergySystem : MonoBehaviour
{
    private EnergySystem energySystem;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [SerializeField] private UnityEvent OutOfEnergy;

    public float Energy
    {
        get => this.energySystem.CurrentEnergy;
        set => this.energySystem.CurrentEnergy = value;
    }

    public void SubscribeToCurrentEnergyChanged(Action<float> callback)
    {
        this.energySystem.CurrentEnergyChanged += callback;
    }
    

    private void Start()
    {
        this.energySystem = this.playerUpgradesState.EnergySystem.EnergySystem;
        this.energySystem.ResetCurrentEnergy();
    }

    public bool TryDebitEnergy(float amountToDebit)
    {
        float newEnergyAmount = this.energySystem.CurrentEnergy - amountToDebit;
        if (newEnergyAmount > 5)
        {
            this.energySystem.CurrentEnergy = newEnergyAmount;
            return true;
        }

        this.energySystem.CurrentEnergy = 0;
        
        this.OutOfEnergy?.Invoke();

        return false;
    }

    public bool HasBudgetFor(float expense)
    {
        return this.energySystem.CurrentEnergy - expense >= 0;
    }

    private void Update()
    {
        this.energySystem.Update();
    }

    public void SubscribeToEnergyPeaked(Action<float> callback)
    {
        this.energySystem.CurrentEnergyPeaked += callback;
    }

    public void Tax(float taxAmount)
    {
        this.energySystem.CurrentEnergy -= taxAmount;
    }
}
