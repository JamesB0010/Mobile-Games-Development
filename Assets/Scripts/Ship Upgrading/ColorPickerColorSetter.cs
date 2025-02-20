using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlexibleColorPicker))]
public class ColorPickerColorSetter : MonoBehaviour
{
    private FlexibleColorPicker colorPicker;
    
    [SerializeField] private ColorReference colorToInitializeTo;

    private void Awake()
    {
        this.colorPicker = GetComponent<FlexibleColorPicker>();
    }

    private void Start()
    {
        this.colorPicker.color = this.colorToInitializeTo.GetValue();
    }

    public void UpdateColor()
    {
        this.colorPicker.color = this.colorToInitializeTo.GetValue();
    }
}
