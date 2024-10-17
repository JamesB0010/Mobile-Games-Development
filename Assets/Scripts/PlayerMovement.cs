using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;


//Responsibilities of this class
//handling inputted throttle and applying speed
//Handling roll input and applying roll
//handling yaw input and applying yaw
//handling pitch input and applying pitch
//setting the boost cam on boost
//handling the boost input and boosting

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera boostCam;
    
    [SerializeField]
    private float speed;

    private float inputtedThrottle;

    private float throttle;
    public float Throttle => throttle;
    [SerializeField] private float throttleChangeRate;

    private float inputtedRoll;
    public float InputtedRoll => inputtedRoll;

    private float inputtedYaw;

    private float inputtedPitch;
    public float InputtedPitch => inputtedPitch;

    [SerializeField] private float rollSpeed;

    [SerializeField] private float pitchSpeed;


    [SerializeField] private float rollSpeedDefault, PitchSpeedDefault;

    [SerializeField] private float yawSpeed;

    [SerializeField] private Camera playerCamera;

    public bool isBoosting;


    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float maxVelocity;

    private float currentMaxVelocity;

    private float maxVelocityDefault;

    [SerializeField] private float maxBoostVelocity;


    public void SetSensitivityAiming()
    {
        this.pitchSpeed = 10;
        this.rollSpeed = 10;
    }

    public void SetSensitivityNormal()
    {
        this.rollSpeed = this.rollSpeedDefault;
        this.pitchSpeed = this.PitchSpeedDefault;
    }


    private void Start()
    {
        this.maxVelocityDefault = this.maxVelocity;
        this.currentMaxVelocity = this.maxVelocity;

        this.PitchSpeedDefault = this.pitchSpeed;
        this.rollSpeedDefault = this.rollSpeed;
    }

    void Update()
    {
        this.throttle = Mathf.Lerp(this.throttle, this.inputtedThrottle, Time.deltaTime * this.throttleChangeRate);
        transform.Rotate(new Vector3(inputtedPitch * Time.deltaTime * this.pitchSpeed, this.inputtedYaw * Time.deltaTime * this.yawSpeed,-inputtedRoll * Time.deltaTime * this.rollSpeed));

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
        this.inputtedYaw = input.x;
    }

    public void OnPitchAndRoll(InputAction.CallbackContext ctx)
    {
        this.inputtedPitch = ctx.ReadValue<Vector2>().y;
        this.inputtedRoll = ctx.ReadValue<Vector2>().x;
    }

    public void OnGyroUpdate(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<Quaternion>());
    }

    public void OnBoost(InputAction.CallbackContext ctx)
    {
        if (ctx.action.IsPressed())
        {
            this.currentMaxVelocity = this.maxBoostVelocity;
            this.boostCam.gameObject.SetActive(true);
            this.isBoosting = true;
        }
        else
        {
            this.currentMaxVelocity = this.maxVelocityDefault;
            this.boostCam.gameObject.SetActive(false);
            this.isBoosting = false;
        }
    }
}
