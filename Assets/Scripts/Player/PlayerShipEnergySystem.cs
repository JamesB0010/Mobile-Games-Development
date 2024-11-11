using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipEnergySystem : MonoBehaviour
{
    private EnergySystem energySystem;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;
    
    //this value is just for debugging
    [SerializeField] private float debug_CurrentEnergy;


    private void Start()
    {
        this.energySystem = this.playerUpgradesState.EnergySystem.EnergySystem;
        this.energySystem = (EnergySystem)this.energySystem.Clone();
        this.energySystem.ResetCurrentEnergy();
        this.debug_CurrentEnergy = this.energySystem.CurrentEnergy;
    }

    public bool TryDebitEnergy(float amountToDebit)
    {
        float newEnergyAmount = this.energySystem.CurrentEnergy - amountToDebit;
        if (newEnergyAmount > 0)
        {
            this.energySystem.CurrentEnergy = newEnergyAmount;
            this.debug_CurrentEnergy = newEnergyAmount;
            return true;
        }

        return false;
    }
}
