using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera playerCam;
    private void Start()
    {
        this.playerCam = FindObjectOfType<Camera>();
        this.OrientToFaceCamera();
    }

    private void Update()
    {
        this.OrientToFaceCamera();
    }

    private void OrientToFaceCamera()
    {
        Vector3 directionToCamera = transform.position - this.playerCam.transform.position;
        transform.forward = directionToCamera.normalized;
    }
}
