using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CrosshairPositioner : MonoBehaviour
{
    private bool firstPerson = true;

    [SerializeField] private Camera thirdPersonCamera;

    [SerializeField] private Transform lookAtPoint;

    private RectTransform imageTransform;
    
    private Vector2 firstPersonCrosshairPosition;

    private void Start()
    {
        this.imageTransform = GetComponent<Image>().GetComponent<RectTransform>();
        this.imageTransform.anchoredPosition = new Vector2(Screen.width, Screen.height) * 0.5f;
        this.firstPersonCrosshairPosition = this.imageTransform.anchoredPosition;
    }


    private void Update()
    {
        if (this.firstPerson)
        {
            this.imageTransform.anchoredPosition = this.firstPersonCrosshairPosition;
        }
        else
        {
            Vector3 screenPoint = this.thirdPersonCamera.WorldToViewportPoint(this.lookAtPoint.position);

            screenPoint.x *= Screen.width;

            screenPoint.y *= Screen.height;

            imageTransform.anchoredPosition = screenPoint;

            Debug.Log(screenPoint);
        }
    }

    public void CameraViewChanged(bool firstPerson)
    {
        this.firstPerson = firstPerson;
    }
}
