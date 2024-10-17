using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipThrottle : MonoBehaviour
{
    private float inputtedThrottle;

    private float throttle;
    public float Throttle => throttle;
    [SerializeField] private float throttleChangeRate;


    private void Update()
    {
        this.throttle = Mathf.Lerp(this.throttle, this.inputtedThrottle, Time.deltaTime * this.throttleChangeRate);
    }

    public void OnThrottleAndYaw(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        float throttleInput = input.y < 0 ? 0 : input.y;
                
        this.inputtedThrottle = throttleInput;
    }
}