using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerShipElevator : MonoBehaviour
{
    [SerializeField] private BoolReference usingGyro;

    [SerializeField] private BoolReference invertPitch;
    private bool UsingGyro => this.usingGyro.GetValue();
    private float inputtedPitch;
    public float InputtedPitch => inputtedPitch;


    [SerializeField] private float pitchSpeed;

    private float pitchSpeedDefault;

    private PlayerInput playerInput;

    private void Start()
    {
        this.pitchSpeedDefault = this.pitchSpeed;
        this.playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        float pitchAmount = this.inputtedPitch * Time.deltaTime * this.pitchSpeed;
        transform.Rotate(new Vector3(pitchAmount, 0, 0));

        ConditionalGetPitchFromGyro();
    }

    private void ConditionalGetPitchFromGyro()
    {
        float gyroPitch = AttitudeInput.GetPitchNormalized();
        if (this.UsingGyro)
        {
            if (this.invertPitch.GetValue())
                this.inputtedPitch = gyroPitch;
            else
                this.inputtedPitch = -gyroPitch;


        }
    }

    public void OnPitchAndRoll(InputAction.CallbackContext ctx)
    {
        if (this.UsingGyro)
            return;
        
        float inputVal = ctx.ReadValue<Vector2>().y;
        if (this.invertPitch.GetValue())
            this.inputtedPitch = inputVal;
        else
            this.inputtedPitch = -inputVal;


    }

    public void OnPitchAndRoll(Vector2 value)
    {
        if (this.invertPitch.GetValue())
            this.inputtedPitch = value.y;
        else
            this.inputtedPitch = -value.y;
    }

    public void SetSensitivityAiming()
    {
        this.pitchSpeed = 10;
    }
    
    public void SetSensitivityHover()
    {
        this.pitchSpeed = 30;
    }

    public void SetSensitivityNormal()
    {
        this.pitchSpeed = this.pitchSpeedDefault;
    }
}
