using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationPlayExposer : MonoBehaviour
{
    private Animation animation;

    private void Start()
    {
        this.animation = GetComponent<Animation>();
    }

    public void Play()
    {
        this.animation.Play();
    }
}
