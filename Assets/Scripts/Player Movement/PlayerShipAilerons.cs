using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerShipAilerons : MonoBehaviour
{
    [SerializeField] private BoolReference usingGyro;
    private bool UsingGyro => this.usingGyro.GetValue();
    private float inputtedRoll;
    public float InputtedRoll => inputtedRoll;


    [SerializeField] private float rollSpeed;

    private float rollSpeedDefault;

    private PlayerInput playerInput;

    private void Start()
    {
        this.rollSpeedDefault = this.rollSpeed;
        this.playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        float rollAmount = -inputtedRoll * Time.deltaTime * this.rollSpeed;
        transform.Rotate(new Vector3(0, 0, rollAmount));

        ConditionalGetRollFromGyro();
    }

    private void ConditionalGetRollFromGyro()
    {
        if (this.UsingGyro)
        {
            this.inputtedRoll = AttitudeInput.GetRollNormalized();
        }
    }

    public void OnPitchAndRoll(InputAction.CallbackContext ctx)
    {
        if (this.UsingGyro)
            return;

        this.inputtedRoll = ctx.ReadValue<Vector2>().x;
    }

    public void SetSensitivityAiming()
    {
        this.rollSpeed = 10;
    }

    public void SetSensitivityNormal()
    {
        this.rollSpeed = this.rollSpeedDefault;
    }
}
