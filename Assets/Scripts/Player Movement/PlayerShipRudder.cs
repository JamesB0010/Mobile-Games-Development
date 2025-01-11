using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipRudder : MonoBehaviour
{
    [SerializeField] private BoolReference usingGyro;
    [SerializeField] private BoolReference simpleInput;
        
    private bool UsingGyro => this.usingGyro.GetValue();
    private float inputtedYaw;

    [SerializeField] private float yawSpeed;

    private float yawSpeedDefault;

    private void Start()
    {
        this.yawSpeedDefault = yawSpeed;
    }

    private void Update()
    {
        float yawAmount = this.inputtedYaw * Time.deltaTime * this.yawSpeed;
        transform.Rotate(new Vector3(0, yawAmount, 0));
        
        
        ConditionalGetRollFromGyro();
    }

    public void OnThrottleAndYaw(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        this.inputtedYaw = input.x;
    }

    private void ConditionalGetRollFromGyro()
    {
        if (this.UsingGyro && this.simpleInput.GetValue())
        {
            this.inputtedYaw = AttitudeInput.GetRollNormalized();
        }
    }

    public void OnThrottleAndYaw(Vector2 value)
    {
        this.inputtedYaw = value.x;
    }
    public void SetSensitivityAiming()
    {
        this.yawSpeed = 5;
    }
    
    public void SetSensitivityHover()
    {
        this.yawSpeed = 15;
    }

    public void SetSensitivityNormal()
    {
        this.yawSpeed = this.yawSpeedDefault;
    }
}
