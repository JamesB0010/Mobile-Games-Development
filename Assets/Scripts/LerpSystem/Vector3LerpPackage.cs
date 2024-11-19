using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Vector3LerpPackage : LerpPackage
{
    public Action<Vector3> lerpStepCallback;

    private Vector3 _start, _target;

    public override object start
    {
        get => this._start;
        set => this._start = (Vector3)value;
    }

    public override object target
    {
        get => this._target;
        set => this._target = (Vector3)value;
    }

    public Vector3LerpPackage(Vector3 start, Vector3 target, Action<Vector3> stepCallback, Action<LerpPackage> finalCb,
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
            Vector3.Lerp(this._start, this._target, this.currentPercentage)
        );
    }
}