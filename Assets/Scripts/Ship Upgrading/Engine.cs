using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Engine")]
public class Engine : ShipItem
{
    [FormerlySerializedAs("speed")] [SerializeField] private float accelerationSpeed;

    [SerializeField] private float energyDrainRate;
    
    [SerializeField] private float topSpeed;
    
    [SerializeField] private float maxVelocity;
    public float MaxVelocity => this.maxVelocity;

    [SerializeField] private EngineBoostStats engineBoostStats;


    public float TopSpeed => this.topSpeed;

    public EngineBoostStats EngineBoostStats => this.engineBoostStats;
    
    


    public float AccelerationSpeed => this.accelerationSpeed;



    public float EnergyDrainRate => this.energyDrainRate;


    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}
