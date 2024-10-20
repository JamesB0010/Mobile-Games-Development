using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Dependencies resolved in start
    private GunSystems gunSystem;
    

    private void Start()
    {
        this.gunSystem = FindObjectOfType<GunSystems>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gunSystem.OnShootButtonActivate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.gunSystem.OnShootButtonDeactiveated();
    }


}
