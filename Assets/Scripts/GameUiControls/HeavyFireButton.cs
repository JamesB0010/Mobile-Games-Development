using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Weapon;

public class HeavyFireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GunSystems gunSystem;

    private void Start()
    {
        this.gunSystem = FindObjectOfType<GunSystems>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gunSystem.TryingToShootHeavy = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.gunSystem.TryingToShootHeavy = false;
    }
}
