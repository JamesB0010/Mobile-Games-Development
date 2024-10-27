using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class CameraFOVChanger : FSMBehaviour
    {
        //Attributes
        [Header("States")][SerializeField] private State idleMove;
        [SerializeField] private State zoom;

        [Header("Configurables")]
        [SerializeField]
        private float zoomRate;

        [SerializeField] private float minFov, maxFov;

        [SerializeField] private UnityEvent ZoomInEvent = new UnityEvent();

        [SerializeField] private UnityEvent ZoomOutEvent = new UnityEvent();

        //dependencies resolved in the start function
        CinemachineVirtualCamera virtualCamera;

        private PlayerShipThrottle playerThrottle;

        private void Start()
        {
            this.playerThrottle = FindObjectOfType<PlayerShipThrottle>();
            this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void ZoomCameraForShooting()
        {
            this.virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(this.virtualCamera.m_Lens.FieldOfView, 25,
                Time.deltaTime * this.zoomRate);
        }

        private void MapThrottleToFOV()
        {
            float throttleAmount = playerThrottle.Throttle;

            this.virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(this.virtualCamera.m_Lens.FieldOfView,
                throttleAmount.MapRange(0, 1, this.minFov, this.maxFov), Time.deltaTime * this.zoomRate);
        }

        public override void Behave(State state)
        {
            if (state.StateName == this.idleMove.name)
            {
                MapThrottleToFOV();
            }
            else if (state.StateName == this.zoom.name)
            {
                this.ZoomCameraForShooting();
            }
        }

        public override void EnterState(State state)
        {
            if (state.StateName == this.idleMove.name)
            {
                this.ZoomOutEvent?.Invoke();
            }
            else if (state.StateName == this.zoom.name)
            {
                this.ZoomInEvent?.Invoke();
            }
        }
    }
}