using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoostButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerShipBooster booster;


    private void Start()
    {
        this.booster = FindObjectOfType<PlayerShipBooster>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.booster.IsBoosting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.booster.IsBoosting = false;
    }

    public void OnEngineEquipped(Engine engine)
    {
        if (!engine.EngineBoostStats.CanBoost)
        {
            this.gameObject.SetActive(false);
        }
    }
}
