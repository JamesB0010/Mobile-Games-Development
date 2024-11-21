using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CrosshairPositioner : MonoBehaviour
{
    private bool firstPerson = true;

    [SerializeField] private Camera uiCamera;
    
    [SerializeField] private Transform lookAtPoint;
    
    private RectTransform imageTransform;
    
    private Vector3 firstPersonCrosshairPosition;

    private void Start()
    {
        this.imageTransform = GetComponent<Image>().GetComponent<RectTransform>();
        this.firstPersonCrosshairPosition = this.imageTransform.position;
    }


    private void Update()
    {
        if (this.firstPerson)
        {
            this.imageTransform.position = this.firstPersonCrosshairPosition;
        }
        else
        {
            Vector3 screenPosition = this.uiCamera.WorldToScreenPoint(this.lookAtPoint.position);

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.imageTransform.parent.GetComponent<RectTransform>(), // Parent RectTransform
                screenPosition,
                this.uiCamera,
                out localPoint
            );
            
            Debug.Log(localPoint);

            this.imageTransform.anchoredPosition = localPoint;
        }
    }

    public void CameraViewChanged(bool firstPerson)
    {
        this.firstPerson = firstPerson;
    }
}
