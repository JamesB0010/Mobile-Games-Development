using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerpPackage : LerpPackage
{
    public event Action<Color> onLerpStep;
    
    public Action<Color> lerpStepCallback;

    private Color _start, _target;

    public override object start
    {
        get => this._start;
        set => this._start = (Color)value;
    }

    public override object target
    {
        get => this._target;
        set => this._target = (Color)value;
    }

    public ColorLerpPackage(Color start, Color target, Action<Color> stepCallback, Action<LerpPackage> finalCb,
        float timeToLerp = 1.0f, AnimationCurve animCurve = null) : base(finalCb, timeToLerp, animCurve)
    {
        this._start = start;
        this._target = target;
        this.lerpStepCallback = stepCallback;
    }

    public override void AddToProcessor(ref LerpPackageProcessor processor)
    {
        processor.AddPackage(this);
    }

    public override void RunStepCallback()
    {
        Color value = Color.Lerp(this._start, this._target, this.currentPercentage);
        this.onLerpStep?.Invoke(value);
        this.lerpStepCallback(value);
    }
}
