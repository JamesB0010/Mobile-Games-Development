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

    [SerializeField] private Animator playerAnimator;
    private static readonly int engineOpenAmountParameterID = Animator.StringToHash("EngineOpenAmount");

    [SerializeField] private FighterShipFlame flame;

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
        
        this.SetEngineOpenAmount();
        this.flame.SetIntensity(this.throttle.Throttle);
        
        return acceleration;
    }

    public void SetEngineOpenAmount()
    {
        if (this.booster.IsBoosting)
        {
            this.playerAnimator.SetFloat(engineOpenAmountParameterID, 1.0f);
        }
        this.playerAnimator.SetFloat(engineOpenAmountParameterID, this.throttle.Throttle);
    }
}

