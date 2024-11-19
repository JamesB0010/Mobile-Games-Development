using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FloatLerpPackage : LerpPackage
{
    public Action<float> lerpStepCallback;

    private float _start, _target;

    public override object start
    {
        get => this._start;
        set => this._start = (float)value;
    }

    public override object target
    {
        get => this._target;
        set => this._target = (float)value;
    }

    public FloatLerpPackage(float start, float target, Action<float> stepCallback, Action<LerpPackage> finalCb,
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
        this.lerpStepCallback(
            Mathf.Lerp(this._start, this._target, this.currentPercentage)
        );
    }
}
