using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraViewButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool firstPerson = true;

    private bool FirstPerson
    {
        get => this.firstPerson;
        set
        {
            firstPersonCamera.SetActive(value);
            foreach (var cam in this.firstPersonVirtualCameras)
            {
                cam.gameObject.SetActive(value);
            }
            
            
            this.thirdPersonCamera.SetActive(!value);
            foreach (var cam in this.thirdPersonVirtualCameras)
            {
                cam.gameObject.SetActive(!value);
            }
            
            this.firstPerson = value;
        }
    }

    [SerializeField] private GameObject firstPersonCamera;

    [SerializeField] private CinemachineVirtualCamera[] firstPersonVirtualCameras;

    [SerializeField] private GameObject thirdPersonCamera;

    [SerializeField] private CinemachineVirtualCamera[] thirdPersonVirtualCameras;
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.FirstPerson = !this.FirstPerson;
    }
}
