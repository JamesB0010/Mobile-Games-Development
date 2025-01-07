using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : EnemyBase
{
    //Attributes
    [SerializeField] private GameObject DeathParticle;

    [Header("Movement")]
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float speed;


    private Quaternion desiredRotation;
    [SerializeField] private float rotationSpeed;

    [Header("Behaviour settings")]
    [SerializeField] private float desiredDirectionChangeInterval = 5;
    private PhasedEventTimeKeeper directionChangeTimeKeeper = new();


    private void Start()
    {
        this.directionChangeTimeKeeper = new PhasedEventTimeKeeper(this.desiredDirectionChangeInterval);
        this.OnDeathReaction = this.OnDeath;
    }

    private void Update()
    {
        TryGenerateNewRotation();

        RotateAndMoveAgent();
    }

    private void TryGenerateNewRotation()
    {
        if (directionChangeTimeKeeper.HasEnoughTimeElapsedSinceEvent())
        {
            GenerateNewRotation();
        }
    }

    private void RotateAndMoveAgent()
    {
        RotateTowardsDesiredRotation();
        Vector3 acceleration = CalculateAcceleration();
        ApplyAccelerationToVelocity(acceleration);
        transform.Translate(this.velocity, Space.World);
    }


    private void GenerateNewRotation()
    {
        this.directionChangeTimeKeeper.UpdateTimestamp();

        this.desiredRotation = Random.rotation;
    }

    private void RotateTowardsDesiredRotation()
    {
        transform.rotation =
            Quaternion.Lerp(transform.rotation, this.desiredRotation, Time.deltaTime * this.rotationSpeed);
    }
    private Vector3 CalculateAcceleration()
    {
        return transform.forward * (Time.deltaTime * speed);
    }

    private void ApplyAccelerationToVelocity(Vector3 acceleration)
    {
        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.maxVelocity);
    }

    private void OnDeath()
    {
        this.enemiesManager.EnemyDied(this);
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
    }
}
