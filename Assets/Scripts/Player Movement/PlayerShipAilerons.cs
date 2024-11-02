using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipAilerons : MonoBehaviour
{
    [SerializeField] private bool usingGyro;
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
        if (this.usingGyro)
        {
            this.inputtedRoll = AttitudeInput.GetRollNormalized();
        }
    }

    public void OnPitchAndRoll(InputAction.CallbackContext ctx)
    {
        if (this.usingGyro)
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
