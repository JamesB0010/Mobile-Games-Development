using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipViewButton : MonoBehaviour
{
    private bool zoomedIn = false;

    [SerializeField] private UnityEvent ReturnToShipViewEvent;

    public void OnShipViewButtonPressed()
    {
        if (zoomedIn)
        {
            this.ReturnToShipViewEvent?.Invoke();
            this.zoomedIn = false;
        }
    }

    public void ZoomIn()
    {
        this.zoomedIn = true;
    }
}
