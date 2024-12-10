using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FighterShipFlame : MonoBehaviour
{
    [SerializeField] private Vector3 lowIntensityPos, highIntensityPos;

    [SerializeField] private float lowIntensitySize, highIntenitySize;

    [SerializeField] private float lowIntensityLifetime, highIntensityLifetime;
    
    private ParticleSystem particleSystem;

    private void Awake()
    {
        this.particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetIntensity(float percentage)
    {
        this.transform.localPosition = (Vector3.Lerp(lowIntensityPos, highIntensityPos, percentage));

        var main = this.particleSystem.main;
        
        main.startSize = Mathf.Lerp(this.lowIntensitySize, highIntenitySize, percentage);

       main.startLifetime = Mathf.Lerp(this.lowIntensityLifetime, this.highIntensityLifetime, percentage);
    }
}
