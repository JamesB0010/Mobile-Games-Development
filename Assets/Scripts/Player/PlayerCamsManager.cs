using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;

public sealed class PlayerCamsManager : MonoBehaviour
{
    [SerializeField] private Camera firstPersonCamera;

    [SerializeField] private CinemachineVirtualCamera firstPersonBoostCam;

    [SerializeField] private CinemachineVirtualCamera[] firstPersonVirtualCameras;


    [SerializeField] private Camera thirdPersonCamera;

    [SerializeField] private CinemachineVirtualCamera thirdPersonBoostCam;

    [SerializeField] private CinemachineVirtualCamera[] thirdPersonVirtualCameras;

    private bool firstPerson = true;

    public void CameraViewChanged(bool firstPerson)
    {
        this.firstPerson = true;
        firstPersonCamera.gameObject.SetActive(firstPerson);
        foreach (var cam in this.firstPersonVirtualCameras)
        {
            cam.gameObject.SetActive(firstPerson);
        }


        this.thirdPersonCamera.gameObject.SetActive(!firstPerson);
        foreach (var cam in this.thirdPersonVirtualCameras)
        {
            cam.gameObject.SetActive(!firstPerson);
        }

        firstPersonBoostCam.gameObject.SetActive(false);
        thirdPersonBoostCam.gameObject.SetActive(false);
    }

    public Camera GetActiveCamera()
    {
        return this.firstPerson
            ? this.firstPersonCamera
            : this.thirdPersonCamera;
    }
}
