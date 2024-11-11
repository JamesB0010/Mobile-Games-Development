using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Engine")]
public class Engine : ShipItem
{
    [SerializeField] private float speed;

    [SerializeField] private bool canBoost;

    [SerializeField] private float energyDrainRate;

    [SerializeField] private float energyBoostDrainRate;
    public float Speed => this.speed;


    public bool CanBoost => this.canBoost;

    public float EnergyDrainRate => this.energyDrainRate;


    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}
