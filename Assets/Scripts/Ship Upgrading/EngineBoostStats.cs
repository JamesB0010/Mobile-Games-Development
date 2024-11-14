using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EngineBoostStats 
{
    
    [SerializeField] private bool canBoost;
    
    
    public bool CanBoost => this.canBoost;
    
    
    [SerializeField] private float energyBoostDrainRate;

    public float EnergyBoostDrainRate => this.energyBoostDrainRate;


    [SerializeField] private float maxBoostVelocity;

    public float MaxBoostVelocity => this.maxBoostVelocity;
}
