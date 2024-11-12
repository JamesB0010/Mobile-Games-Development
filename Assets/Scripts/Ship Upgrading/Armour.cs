using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Armour")]
public class Armour : ShipItem
{
    [SerializeField] private float energyUsedPerHit;

    public float EnergyUsedPerHit => this.energyUsedPerHit;

    [SerializeField] private float minimumOperationalEnergyLevel;

    public float MinimumOperationalEnergyLevel => this.minimumOperationalEnergyLevel;

    [Range(0,1)] [SerializeField] private float damageDampeningMultiplier;

    public float DamageDampeningMultiplier => this.damageDampeningMultiplier;
    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
    
    private float CalculateDamageToPlayer(float damageIn, float energyLevel)
    {
        if (energyLevel < this.minimumOperationalEnergyLevel)
        {
            return damageIn;
        }
        return damageIn *= this.damageDampeningMultiplier;
    }

    public float TaxEnergyFromHit(float playerEnergyAmount)
    {
        if (playerEnergyAmount < this.minimumOperationalEnergyLevel)
            return playerEnergyAmount;
        
        return playerEnergyAmount - energyUsedPerHit;
    }
}
