using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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

    [SerializeField] private float yawSpeed;

    [SerializeField] private Camera playerCamera;


    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float maxVelocity;
    
    void Update()
    {
        this.throttle = Mathf.Lerp(this.throttle, this.inputtedThrottle, Time.deltaTime * this.throttleChangeRate);
        transform.Rotate(new Vector3(inputtedPitch * Time.deltaTime * this.pitchSpeed, this.inputtedYaw * Time.deltaTime * this.yawSpeed,-inputtedRoll * Time.deltaTime * this.rollSpeed));

        Vector3 acceleration = Vector3.zero;
        acceleration += this.playerCamera.transform.forward * (speed * this.throttle);
        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, maxVelocity);
        transform.Translate(this.velocity, Space.World);
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
}
