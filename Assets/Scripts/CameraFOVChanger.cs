using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{
    CinemachineVirtualCamera camera;
    
    private PlayerMovement playerMovement;

    [SerializeField]
    private float minFov, maxFov;
    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
        this.camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        float throttleAmount = playerMovement.Throttle;

        this.camera.m_Lens.FieldOfView = ValueInRangeMapper.Map(throttleAmount, 0, 1, this.minFov, this.maxFov);
    }
}
