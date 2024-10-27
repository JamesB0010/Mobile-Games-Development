using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipRudder : MonoBehaviour
{
    private float inputtedYaw;

    [SerializeField] private float yawSpeed;

    private void Update()
    {
        float yawAmount = this.inputtedYaw * Time.deltaTime * this.yawSpeed;
        transform.Rotate(new Vector3(0, yawAmount, 0));
    }

    public void OnThrottleAndYaw(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        this.inputtedYaw = input.x;
    }
}
