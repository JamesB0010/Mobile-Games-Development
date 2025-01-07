using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorPickerExpandContractor : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] FlexibleColorPicker colorPicker;

    [SerializeField] private pointerDownPassthrough colorPickerBackground;

    [SerializeField] private float expansionContractionTime = 0.5f;

    [SerializeField] private UnityEvent pointerDown;

    [SerializeField] private ColorReference startingColor;

    private bool expanded;

    private Image image;

    private bool Expanded
    {
        get => this.expanded;

        set
        {
            if (this.expanded == value)
                return;


            this.expanded = value;


            if (this.expanded)
            {
                this.pointerDown?.Invoke();
                colorPicker.transform.localScale.LerpTo(new Vector3(2,2,2), this.expansionContractionTime, val => colorPicker.transform.localScale = val, pkg =>
                {
                    colorPicker.enabled = true;
                    this.colorPickerBackground.gameObject.SetActive(true);
                }, GlobalLerpProcessor.easeInOutCurve);
            }
            else
            {
                this.pointerDown?.Invoke();
                colorPicker.enabled = false;
                        this.colorPickerBackground.gameObject.SetActive(false);
                colorPicker.transform.localScale.LerpTo(Vector3.zero, this.expansionContractionTime, val => colorPicker.transform.localScale = val, null, GlobalLerpProcessor.easeInOutCurve);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.Expanded = !this.Expanded;
    }


    private void Awake()
    {
        this.image = GetComponent<Image>();
        this.colorPickerBackground.pointerDown = () =>
        {
            this.Expanded = false;
        };
    }

    private void Start()
    {
        this.image.color = this.startingColor.GetValue();
    }
}
