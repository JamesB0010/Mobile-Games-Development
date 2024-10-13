using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickRotator : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float minX, maxX, minZ, maxZ;

    [SerializeField] private float stickMovementSpeed;

    private void Update()
    {
        float pitchAmount = playerMovement.InputtedPitch;
        float rollAmount = playerMovement.InputtedRoll;
        
        float targetZRotation = ValueInRangeMapper.Map(rollAmount, -1, 1, minZ, maxZ);
        float targetXRotation = ValueInRangeMapper.Map(pitchAmount, -1, 1, minX, maxX);
            
        Quaternion localRotation = transform.localRotation;
            
        Quaternion targetRotation = Quaternion.Lerp(localRotation, Quaternion.Euler(new Vector3(targetXRotation, localRotation.eulerAngles.y,  targetZRotation)),Time.deltaTime * this.stickMovementSpeed);
        
        transform.localRotation = targetRotation;
    }
}
