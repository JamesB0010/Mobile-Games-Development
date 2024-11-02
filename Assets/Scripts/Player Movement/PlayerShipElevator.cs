using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipElevator : MonoBehaviour
{
    [SerializeField] private bool usingGyro;
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
        if (this.usingGyro)
        {
            this.inputtedPitch = -AttitudeInput.GetPitchNormalized();
        }
    }

    public void OnPitchAndRoll(InputAction.CallbackContext ctx)
    {
        if (this.usingGyro)
            return;

        this.inputtedPitch = ctx.ReadValue<Vector2>().y;
    }

    public void SetSensitivityAiming()
    {
        this.pitchSpeed = 10;
    }

    public void SetSensitivityNormal()
    {
        this.pitchSpeed = this.pitchSpeedDefault;
    }
}
