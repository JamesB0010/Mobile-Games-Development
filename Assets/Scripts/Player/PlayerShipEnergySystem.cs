using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipEnergySystem : MonoBehaviour
{
    private EnergySystem energySystem;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    public void SubscribeToCurrentEnergyChanged(Action<float> callback)
    {
        this.energySystem.CurrentEnergyChanged += callback;
    }
    

    private void Start()
    {
        this.energySystem = this.playerUpgradesState.EnergySystem.EnergySystem;
        this.energySystem = (EnergySystem)this.energySystem.Clone();
        this.energySystem.ResetCurrentEnergy();
    }

    public bool TryDebitEnergy(float amountToDebit)
    {
        float newEnergyAmount = this.energySystem.CurrentEnergy - amountToDebit;
        if (newEnergyAmount > 0)
        {
            this.energySystem.CurrentEnergy = newEnergyAmount;
            return true;
        }

        this.energySystem.CurrentEnergy = 0;

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
}
