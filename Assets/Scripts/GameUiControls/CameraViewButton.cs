using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraViewButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool firstPerson = true;

    [SerializeField] private UnityEvent<bool> ChangeCameraView = new UnityEvent<bool>();
    
    private bool FirstPerson
    {
        get => this.firstPerson;
        set
        {
            this.firstPerson = value;
            this.ChangeCameraView?.Invoke(value);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.FirstPerson = !this.FirstPerson;
    }
}
