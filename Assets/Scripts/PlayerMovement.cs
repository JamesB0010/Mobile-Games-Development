using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


//Responsibilities of this class
//handling inputted throttle and applying speed
//setting the boost cam on boost

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera boostCam;

    public CinemachineVirtualCamera BoostCam => this.boostCam;
    
    [SerializeField]
    private float speed;

    private float inputtedThrottle;

    private float throttle;
    public float Throttle => throttle;
    [SerializeField] private float throttleChangeRate;

    



    [SerializeField] private Camera playerCamera;



    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float maxVelocity;
    public float MaxVelocity => this.maxVelocity;

    private float currentMaxVelocity;

    public float CurrentMaxVelocity
    {
        set => currentMaxVelocity = value;
    }




    private void Start()
    {
        this.currentMaxVelocity = this.maxVelocity;
    }

    void Update()
    {
        this.throttle = Mathf.Lerp(this.throttle, this.inputtedThrottle, Time.deltaTime * this.throttleChangeRate);

        Vector3 acceleration = Vector3.zero;
        acceleration += this.playerCamera.transform.forward * (speed * this.throttle * Time.deltaTime);
        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.currentMaxVelocity);
        transform.Translate(this.velocity * Time.deltaTime, Space.World);
    }

    public void OnThrottleAndYaw(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        float throttleInput = input.y < 0 ? 0 : input.y;
        
        this.inputtedThrottle = throttleInput;
    }

    public void OnGyroUpdate(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<Quaternion>());
    }

    public void OnBoost(InputAction.CallbackContext ctx)
    {
        
    }
}
