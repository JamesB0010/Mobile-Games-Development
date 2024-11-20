using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamsManager : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCamera;
    
        [SerializeField] private CinemachineVirtualCamera firstPersonBoostCam;
    
        [SerializeField] private CinemachineVirtualCamera[] firstPersonVirtualCameras;
        
    
        [SerializeField] private GameObject thirdPersonCamera;
    
        [SerializeField] private CinemachineVirtualCamera thirdPersonBoostCam;
    
        [SerializeField] private CinemachineVirtualCamera[] thirdPersonVirtualCameras;

        public void CameraViewChanged(bool firstPerson)
        {
            firstPersonCamera.SetActive(firstPerson);
            foreach (var cam in this.firstPersonVirtualCameras)
            {
                cam.gameObject.SetActive(firstPerson);
            }


            this.thirdPersonCamera.SetActive(!firstPerson);
            foreach (var cam in this.thirdPersonVirtualCameras)
            {
                cam.gameObject.SetActive(!firstPerson);
            }

            firstPersonBoostCam.gameObject.SetActive(false);
            thirdPersonBoostCam.gameObject.SetActive(false);
        }
}
