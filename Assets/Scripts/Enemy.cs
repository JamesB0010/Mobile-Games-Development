using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : EnemyBase
{
    [SerializeField] private GameObject DeathParticle;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float speed;

    [SerializeField] private float maxVelocity;

    private Quaternion enemyRotation;

    [SerializeField] private float rotationSpeed;
    
    private float defaultRotationSpeed;


    private float desiredDirectionChangeTimestamp = -100;
    [SerializeField] private float desiredDirectionChangeInterval = 5;

    private GameManager gameManager;

    public GameManager GameManager
    {
        set => this.gameManager = value;
    }

    private void Start()
    {
        this.defaultRotationSpeed = this.rotationSpeed;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad - this.desiredDirectionChangeTimestamp > this.desiredDirectionChangeInterval)
        {
            GenerateNewRotation();
        }

        if (Physics.Raycast(transform.position + transform.forward * 20, transform.forward, out RaycastHit hit))
        {
            if (hit.distance < 50)
            {
                this.GenerateNewRotation();
                //this.rotationSpeed = 10;
            }
        }

        transform.rotation =
            Quaternion.Lerp(transform.rotation, this.enemyRotation, Time.deltaTime * this.rotationSpeed);
        Vector3 acceleration = transform.forward * (Time.deltaTime * speed);
        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.maxVelocity);
        transform.Translate(this.velocity, Space.World);
    }

    private void GenerateNewRotation()
    {
        this.desiredDirectionChangeTimestamp = Time.timeSinceLevelLoad;

        this.enemyRotation = Random.rotation;
    }

    protected override void OnDeath()
    {
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
        this.gameManager.EnemyKilled();
        Destroy(this.gameObject);
    }
}
