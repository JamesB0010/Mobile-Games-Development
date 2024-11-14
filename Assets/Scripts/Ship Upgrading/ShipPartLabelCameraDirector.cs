using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShipPartLabelCameraDirector : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    public void DisableAllCameras()
    {
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
    }
}
