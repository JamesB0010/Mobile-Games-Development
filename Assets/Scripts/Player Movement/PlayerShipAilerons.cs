using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipAilerons : MonoBehaviour
{
    private float inputtedRoll;
    public float InputtedRoll => inputtedRoll;


    [SerializeField] private float rollSpeed;

    private float rollSpeedDefault;

    private void Start()
    {
        this.rollSpeedDefault = this.rollSpeed;
    }

    private void Update()
    {
        float rollAmount = -inputtedRoll * Time.deltaTime * this.rollSpeed;
        transform.Rotate(new Vector3(0, 0, rollAmount));
        Debug.Log(AttitudeInput.RollNormalized);
    }

    public void OnPitchAndRoll(InputAction.CallbackContext ctx)
    {
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
