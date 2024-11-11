using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Engine")]
public class Engine : ShipItem
{
    [SerializeField] private float speed;

    [SerializeField] private float energyDrainRate;

    [SerializeField] private EngineBoostStats engineBoostStats;

    public EngineBoostStats EngineBoostStats => this.engineBoostStats;
    
    


    public float Speed => this.speed;



    public float EnergyDrainRate => this.energyDrainRate;


    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}
