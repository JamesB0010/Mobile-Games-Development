using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.Gyroscope;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Vector3 velocity = Vector3.zero;
    private float currentMaxVelocity;
    public float CurrentMaxVelocity
    {
        set => currentMaxVelocity = value;
    }

    private Gyroscope gyro;
    
    [Header("Configurables")]
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;
    public float MaxVelocity => this.maxVelocity;
    //Dependencies resolved in start
    private PlayerShipThrottle playerThrottle;
    private void Start()
    {
        //gyro = Input.gyro;
        //gyro.enabled = true;
        this.currentMaxVelocity = this.maxVelocity;
        this.playerThrottle = FindObjectOfType<PlayerShipThrottle>();

        //if (UnityEngine.InputSystem.Gyroscope.current != null)
        //{
            //InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        //}

        //if (AttitudeSensor.current != null)
        //{
            //InputSystem.EnableDevice(AttitudeSensor.current);
        //}
    }
    void Update()
    {
        //Debug.Log(gyro.attitude);
        //Debug.Log(UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue());
        //Debug.Log(AttitudeSensor.current.attitude.ReadValue());
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
