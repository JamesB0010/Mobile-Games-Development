using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    //Dependencies
    [SerializeField] private Camera playerCamera;
    
    //Attributes
    private Vector3 velocity = Vector3.zero;
    private float currentMaxVelocity;
    public float CurrentMaxVelocity
    {
        set => currentMaxVelocity = value;
    }
    
    
    
    [Header("Configurables")]
    [SerializeField] private float speed;

    [SerializeField] private float maxVelocity;
    public float MaxVelocity => this.maxVelocity;



    //Dependencies resolved in start
    private PlayerShipThrottle playerThrottle;



    private void Start()
    {
        this.currentMaxVelocity = this.maxVelocity;
        this.playerThrottle = FindObjectOfType<PlayerShipThrottle>();
    }

    void Update()
    {
        var acceleration = CalculateAcceleration();
        AddAccelerationToVelocity(acceleration);
        transform.Translate(this.velocity * Time.deltaTime, Space.World);
    }

    private Vector3 CalculateAcceleration()
    {
        Vector3 acceleration = Vector3.zero;
        acceleration += this.playerCamera.transform.forward * (speed * this.playerThrottle.Throttle * Time.deltaTime);
        return acceleration;
    }
    private void AddAccelerationToVelocity(Vector3 acceleration)
    {
        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.currentMaxVelocity);
    }
}
