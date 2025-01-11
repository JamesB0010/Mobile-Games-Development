using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class pointerDownPassthrough : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent pointerDown;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        this.pointerDown?.Invoke();
    }
}
