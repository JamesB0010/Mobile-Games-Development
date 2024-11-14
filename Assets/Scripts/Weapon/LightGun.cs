using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Guns/Light Gun")]
public class LightGun : Gun
{
    [SerializeField] private float energyExpensePerShot;

    public float EnergyExpensePerShot => this.energyExpensePerShot;
    
    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}


