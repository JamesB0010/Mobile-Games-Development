using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShipEngine : MonoBehaviour
{
    private Engine engine;


    [SerializeField] private Transform playerCamera;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [SerializeField] private PlayerShipBooster booster;

    [SerializeField] private PlayerShipThrottle throttle;
    
    [SerializeField] PlayerShipEnergySystem energySystem;
    public bool CanBoost => this.engine.EngineBoostStats.CanBoost;
    public float MaxVelocity => this.engine.MaxVelocity;
    public float MaxBoostVelocity => this.engine.EngineBoostStats.MaxBoostVelocity;

    [SerializeField] private UnityEvent<Engine> EngineEquippedEvent = new UnityEvent<Engine>();

    [SerializeField] private ParticleSystem engineParticle;

    private void Start()
    {
        this.engine = this.playerUpgradesState.Engine.Engine;
        this.EngineEquippedEvent?.Invoke(this.engine);
        
    }

    public Vector3 CalculateAcceleration()
    {
        Vector3 acceleration = Vector3.zero;

        bool enoughEnergy = this.energySystem.TryDebitEnergy(this.engine.EnergyDrainRate * this.throttle.Throttle * Time.deltaTime);
        if (!enoughEnergy)
            return Vector3.zero;

        if (this.booster.IsBoosting)
            acceleration += this.playerCamera.forward * (engine.AccelerationSpeed * 1 * Time.deltaTime);
        else
            acceleration += this.playerCamera.forward * (engine.AccelerationSpeed * this.throttle.Throttle * Time.deltaTime);
        
        return acceleration;
    }
}

