using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pointerDownPassthrough : MonoBehaviour, IPointerDownHandler
{
    public Action pointerDown = delegate {};
    
    public void OnPointerDown(PointerEventData eventData)
    {
        this.pointerDown.Invoke();
    }
}
