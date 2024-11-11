using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Armour")]
public class Armour : ShipItem
{
    [SerializeField] private float EnergyUsedPerHit;

    [Range(0,1)] [SerializeField] private float damageDampeningMultiplier;
    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }

    private float CalculateDamageToPlayer(float damageIn)
    {
        return damageIn *= this.damageDampeningMultiplier;
    }
}
