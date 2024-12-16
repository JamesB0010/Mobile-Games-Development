using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Weapon;

public class ZoomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Dependencies resolved in start
    private CameraFOVChanger fovChanger;

    [SerializeField] private BoolReference zoomingIn;


    private void Start()
    {
        this.fovChanger = FindObjectOfType<CameraFOVChanger>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.fovChanger.ZoomIn = true;
        this.zoomingIn.SetValue(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.fovChanger.ZoomIn = false;
        this.zoomingIn.SetValue(false);
    }


}
