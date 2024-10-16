using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    
    private PlayerMovement playerMovement;

    public bool zoomCamera = false;

    [SerializeField] private float zoomRate;

    [SerializeField]
    private float minFov, maxFov;
    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (this.zoomCamera)
        {
            this.virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(this.virtualCamera.m_Lens.FieldOfView, 25, Time.deltaTime * this.zoomRate);
            return;
        }
        
        float throttleAmount = playerMovement.Throttle;

        this.virtualCamera.m_Lens.FieldOfView = ValueInRangeMapper.Map(throttleAmount, 0, 1, this.minFov, this.maxFov);

    }
}
