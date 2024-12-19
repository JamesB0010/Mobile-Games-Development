using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipArmour : MonoBehaviour
{
    private Armour armour;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [SerializeField] private PlayerShipEnergySystem energySystem;
    
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        this.armour = this.playerUpgradesState.Armour.Armour;
    }


    public void DealDamage(float damageIn)
    {
        float currentPlayerEnergy = this.energySystem.Energy;
        this.playerHealth.Health -= this.armour.CalculateDamageToPlayer(damageIn, currentPlayerEnergy);
        float newEnergy = this.armour.TaxEnergyFromHit(currentPlayerEnergy);
        this.energySystem.Energy = newEnergy;
    }
}
