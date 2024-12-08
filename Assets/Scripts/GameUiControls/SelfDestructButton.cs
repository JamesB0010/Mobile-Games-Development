using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelfDestructButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent SelfDestructEvent = new UnityEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Self destruct");
        this.SelfDestructEvent?.Invoke();
    }
}
