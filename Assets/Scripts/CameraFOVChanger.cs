using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CameraFOVChanger : FSMBehaviour
{
    //Attributes
    public bool zoomCamera = false;

    [Header("States")] [SerializeField] private State idleMove;
    [SerializeField] private State zoom;
    
    [Header("Configurables")]
    [SerializeField] private float zoomRate;

    [SerializeField]
    private float minFov, maxFov;

    [SerializeField] private BoolReference lookingAtEnemy;

    [SerializeField] private FloatReference timeSpentLookingAtEnemy;

    [SerializeField] private UnityEvent ZoomInEvent = new UnityEvent();

    [SerializeField] private UnityEvent ZoomOutEvent = new UnityEvent();
    
    //dependencies resolved in the start function
    CinemachineVirtualCamera virtualCamera;
    
    private PlayerMovement playerMovement;
    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void ZoomCameraForShooting()
    {
        this.virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(this.virtualCamera.m_Lens.FieldOfView, 25, Time.deltaTime * this.zoomRate);
    }
    private void MapThrottleToFOV()
    {
        float throttleAmount = playerMovement.Throttle;

        this.virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(this.virtualCamera.m_Lens.FieldOfView, ValueInRangeMapper.Map(throttleAmount, 0, 1, this.minFov, this.maxFov), Time.deltaTime * this.zoomRate);
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

    public override bool EvaluateTransition(State current, State to)
    {
        bool lookingAtAnyEnemy = Convert.ToBoolean(this.lookingAtEnemy.GetValue());
        bool beenLookingAtEnemyLongEnough = (float)this.timeSpentLookingAtEnemy.GetValue() > 0.6f;
        if (lookingAtAnyEnemy && beenLookingAtEnemyLongEnough)
            return true;
        
        
        return false;
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
