using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Weapon;
[RequireComponent(typeof(GunSystems))]
public class GunSystemFSMBehaviour : FSMBehaviour
{
    [Header("States")]
    [Header("Light Guns")]
    [SerializeField] private State lightIdle;
    [SerializeField] private State lightShooting;


    private GunSystems gunSystem;

    private void Start()
    {
        this.gunSystem = GetComponent<GunSystems>();
    }

    public override void Behave(State state)
    {
        if (state.StateName == lightIdle.name)
            this.gunSystem.TryingToShootLight = false;

        if (state.StateName == lightShooting.name)
        {
            this.gunSystem.TryingToShootLight = true;
        }
    }
}
