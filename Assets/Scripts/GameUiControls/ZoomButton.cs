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


    private void Start()
    {
        this.fovChanger = FindObjectOfType<CameraFOVChanger>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.fovChanger.ZoomIn = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.fovChanger.ZoomIn = false;
    }


}
