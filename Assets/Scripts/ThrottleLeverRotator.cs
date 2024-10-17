using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLeverRotator : MonoBehaviour
{
    //Attributes
    private PlayerMovement playerMovement;
    
    //configurables
    [SerializeField] private float minXRotation, maxXRotation;


    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        float throttleAmount = playerMovement.Throttle;
        float targetXRotation = ValueInRangeMapper.Map(throttleAmount, 0, 1, minXRotation, maxXRotation);
    
        Quaternion localRotation = transform.localRotation;
    
        Quaternion targetRotation = Quaternion.Euler(new Vector3(targetXRotation, localRotation.eulerAngles.y, localRotation.eulerAngles.z));

        transform.localRotation = targetRotation;
    }

}
