using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool startState;
    private Slider slider;

    private bool locked = false;

    [Header("Animation")] [SerializeField, Range(0, 1f)]
    private float animationDuration = 0.25f;

    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    
    [Header("Events")] [SerializeField] private UnityEvent ToggleOnEvent = new UnityEvent();
    [SerializeField] private UnityEvent ToggleOffEvent = new UnityEvent();

    private bool currentValue = false;
    public bool CurrentValue {
        set
        {
            this.currentValue = value;
            this.slider.value = this.BoolToFloat(value);
            
            this.SetupLerpPackages();
            this.UpdateToggleColors();
        }
    }

    private FloatLerpPackage movementLerpPackage;

    private ColorLerpPackage backgroundLerpPackage, knobLerpPackage;

    [SerializeField] private Image background;
    [SerializeField] private Image knob;


    [SerializeField] private Color backgroundColorFrom, backgroundColorTo, knobColorFrom,  knobColorTo;

    private Dictionary<bool, Color> backgroundColorFromTo;

    private Dictionary<bool, Color> knobColorFromTo;

    private void OnValidate()
    {
        SetupToggleComponent();

        slider.value = this.BoolToFloat(this.startState);
        this.currentValue = this.startState;

        UpdateToggleColors();
    }

    private void UpdateToggleColors()
    {
        if (this.currentValue)
        {
            this.background.color = this.backgroundColorTo;
            this.knob.color = this.knobColorTo;
        }
        else
        {
            this.background.color = this.backgroundColorFrom;
            this.knob.color = this.knobColorFrom;
        }
    }

    private float BoolToFloat(bool val)
    {
        return val ? 1 : 0;
    }
    
    private void SetupToggleComponent()
    {
        this.slider = GetComponent<Slider>();

        this.slider.interactable = false;
        var sliderColours = slider.colors;
        sliderColours.disabledColor = Color.white;
        slider.colors = sliderColours;
        this.slider.transition = Selectable.Transition.None;
    }

    private void Awake()
    {
        this.backgroundColorFromTo = new Dictionary<bool, Color>()
        {
            { false, this.backgroundColorFrom },
            { true, this.backgroundColorTo }
        };

        this.knobColorFromTo = new Dictionary<bool, Color>()
        {
            { false, this.knobColorFrom },
            { true, this.knobColorTo }
        };
        
        this.SetupToggleComponent();
        
    }

    private void SetupLerpPackages()
    {
        this.movementLerpPackage = new FloatLerpPackage(this.BoolToFloat(this.currentValue),
            this.BoolToFloat(!this.currentValue),
            value => { this.slider.value = value; }, pkg =>
            {
                this.locked = false;
                pkg.Reverse();
            }, this.animationDuration, this.slideEase);

        this.backgroundLerpPackage = new ColorLerpPackage(this.backgroundColorFromTo[this.currentValue],
            this.backgroundColorFromTo[!this.currentValue], value => { this.background.color = value; },
            pkg => { pkg.Reverse(); }, this.animationDuration, this.slideEase);

        this.knobLerpPackage = new ColorLerpPackage(this.knobColorFromTo[this.currentValue],
            this.knobColorFromTo[!this.currentValue], value => { this.knob.color = value; }, pkg => { pkg.Reverse(); },
            this.animationDuration, this.slideEase);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.Toggle();
    }

    private void Toggle()
    {
        if (this.locked)
            return;

        this.locked = !this.locked;
        this.currentValue = !this.currentValue;
        
        if (this.currentValue)
        {
            this.ToggleOnEvent?.Invoke();
            Debug.Log("Toggle On");
        }
        else
        {
            this.ToggleOffEvent?.Invoke();
            Debug.Log("Toggle Off");
        }

        GlobalLerpProcessor.AddLerpPackage(this.movementLerpPackage);
        GlobalLerpProcessor.AddLerpPackage(this.backgroundLerpPackage);
        GlobalLerpProcessor.AddLerpPackage(this.knobLerpPackage);
    }
}

