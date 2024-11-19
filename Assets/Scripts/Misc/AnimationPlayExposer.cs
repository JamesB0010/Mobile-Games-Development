using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationPlayExposer : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        this._animation = GetComponent<Animation>();
    }

    public void Play()
    {
        this._animation.Play();
    }
}
