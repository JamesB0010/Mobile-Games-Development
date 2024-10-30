using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class CameraFOVChanger : FSMBehaviour
    {
        //Attributes
        [Header("States")][SerializeField] private State idleMove;
        [SerializeField] private State zoom;


        [SerializeField] private BoolReference isBoosting;

        private bool zoomIn;

        public bool ZoomIn
        {
            set => this.zoomIn = value;
        }

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

        public override bool EvaluateTransition(State current, State to)
        {
            if (current.StateName == this.idleMove.name && this.zoomIn && !(Convert.ToBoolean(this.isBoosting.GetValue())))
            {
                return true;
            }

            if (current.StateName == this.zoom.name)
            {
                return !this.zoomIn;
            }

            return false;
        }

        public void OnShoot(InputAction.CallbackContext ctx)
        {
            if (ctx.action.IsPressed())
            {
                this.zoomIn = true;
            }
            else
            {
                this.zoomIn = false;
            }
        }

        public void OnShootButtonActivate()
        {
            this.zoomIn = true;
        }

        public void OnShootButtonDeactiveated()
        {
            this.zoomIn = false;
        }
    }
}