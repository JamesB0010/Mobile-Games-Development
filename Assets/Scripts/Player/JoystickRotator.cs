using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickRotator : MonoBehaviour
{

    private PlayerShipElevator pitchControls;

    private PlayerShipAilerons rollControls;
    
    [SerializeField] private float minX, maxX, minZ, maxZ;

    [SerializeField] private float stickMovementSpeed;

    private void Start()
    {
        this.pitchControls = FindObjectOfType<PlayerShipElevator>();
        this.rollControls = FindObjectOfType<PlayerShipAilerons>();
    }

    private void Update()
    {
        float pitchAmount = pitchControls.InputtedPitch;
        float rollAmount = rollControls.InputtedRoll;
        
        float targetZRotation = rollAmount.MapRange(-1, 1, minZ, maxZ);
        float targetXRotation = pitchAmount.MapRange(-1, 1, minX, maxX);
            
        Quaternion localRotation = transform.localRotation;
            
        Quaternion targetRotation = Quaternion.Lerp(localRotation, Quaternion.Euler(new Vector3(targetXRotation, localRotation.eulerAngles.y,  targetZRotation)),Time.deltaTime * this.stickMovementSpeed);
        
        
        transform.localRotation = targetRotation;
        
    }
}
