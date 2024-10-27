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
    [SerializeField] private float rotationSpeed;

    private Vector2 lastPos;
    private float mouseDownTimestamp = -10000;

    public void MouseDown(Vector2 pos)
    {
        this.mouseDownTimestamp = Time.timeSinceLevelLoad;
    }
    public void OnScreenPos(Vector2 pos)
    {
        if (Time.timeSinceLevelLoad - this.mouseDownTimestamp > 0.1f)
        {
            this.lastPos = pos;
        }
        Vector2 Delta = pos - this.lastPos;
        float rotationMagnitudeY = Delta.x;
        float rotationMagnitudeZ = Delta.y;
        this.lastPos = pos;
        this.vCam.transform.RotateAround(this.rotateAroundPoint.position, new UnityEngine.Vector3(0, 1, 0), rotationMagnitudeY * Time.deltaTime * this.rotationSpeed);
        this.vCam.transform.RotateAround(this.rotateAroundPoint.position, new UnityEngine.Vector3(0, 0, 1), rotationMagnitudeZ * Time.deltaTime * this.rotationSpeed);
    }
}
