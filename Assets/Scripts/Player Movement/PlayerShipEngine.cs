using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipEngine : MonoBehaviour
{
    private Engine engine;


    [SerializeField] private Transform playerCamera;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [SerializeField] private PlayerShipBooster booster;

    [SerializeField] private PlayerShipThrottle throttle;
    
    [SerializeField] PlayerShipEnergySystem energySystem;
    public bool CanBoost => this.engine.CanBoost;

    private void Start()
    {
        this.engine = this.playerUpgradesState.Engine.Engine;
        this.engine = (Engine)this.engine.Clone();
    }

    public Vector3 CalculateAcceleration()
    {
        Vector3 acceleration = Vector3.zero;

        bool enoughEnergy = this.energySystem.TryDebitEnergy(this.engine.EnergyDrainRate * Time.deltaTime);
        if (!enoughEnergy)
            return Vector3.zero;

        if (this.booster.IsBoosting)
            acceleration += this.playerCamera.forward * (engine.Speed * 1 * Time.deltaTime);
        else
            acceleration += this.playerCamera.forward * (engine.Speed * this.throttle.Throttle * Time.deltaTime);

        return acceleration;
    }
}

