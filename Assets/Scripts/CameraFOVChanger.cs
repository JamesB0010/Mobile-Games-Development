using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{
    //Attributes
    public bool zoomCamera = false;
    
    [Header("Configurables")]
    [SerializeField] private float zoomRate;

    [SerializeField]
    private float minFov, maxFov;
    
    //dependencies resolved in the start function
    CinemachineVirtualCamera virtualCamera;
    
    private PlayerMovement playerMovement;
    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (this.zoomCamera)
        {
            this.ZoomCameraForShooting();
            return;
        }

        MapThrottleToFOV();
    }

    private void ZoomCameraForShooting()
    {
        this.virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(this.virtualCamera.m_Lens.FieldOfView, 25, Time.deltaTime * this.zoomRate);
    }
    private void MapThrottleToFOV()
    {
        float throttleAmount = playerMovement.Throttle;

        this.virtualCamera.m_Lens.FieldOfView = ValueInRangeMapper.Map(throttleAmount, 0, 1, this.minFov, this.maxFov);
    }
}
