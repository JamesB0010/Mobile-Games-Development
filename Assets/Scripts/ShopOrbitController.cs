using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Vector3 = System.Numerics.Vector3;

public class ShopOrbitController : MonoBehaviour
{
    [SerializeField] private Transform rotateAroundPoint;

    [SerializeField] private CinemachineVirtualCamera vCam;

    [SerializeField] private InputAction screenPos;
    
    [SerializeField] private InputAction click;

    private Vector2 lastPos;

    private float mouseDownTimestamp = -10000;

    [SerializeField] private float rotationSpeed;


    private void Awake()
    {
        screenPos.Enable();
        click.Enable();
    }

    private void Start()
    {
        screenPos.performed += this.OnScreenPos;

        click.started += MouseDown;

    }

    
    private void MouseDown(InputAction.CallbackContext ctx)
    {
        this.mouseDownTimestamp = Time.timeSinceLevelLoad;
    }
    
    private void OnScreenPos(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        if (Time.timeSinceLevelLoad - this.mouseDownTimestamp > 0.1f)
        {
            this.lastPos = value;
        }

        Vector2 Delta = value - this.lastPos;

        float rotationMagnitudeY = Delta.x;
        float rotationMagnitudeZ = Delta.y;
        
        this.lastPos = value;
        
        this.vCam.transform.RotateAround(this.rotateAroundPoint.position, new UnityEngine.Vector3(0,1,0), rotationMagnitudeY * Time.deltaTime * this.rotationSpeed);
        this.vCam.transform.RotateAround(this.rotateAroundPoint.position, new UnityEngine.Vector3(0,0,1), rotationMagnitudeZ * Time.deltaTime * this.rotationSpeed);
    }
}
