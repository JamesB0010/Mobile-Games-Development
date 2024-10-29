using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;
[RequireComponent(typeof(GunSystems))]
public class GunSystemFSMBehaviour : FSMBehaviour
{
    [Header("States")] 
    [SerializeField] private State shooting;


    private GunSystems gunSystem;

    private void Start()
    {
        this.gunSystem = GetComponent<GunSystems>();
    }

    public override void Behave(State state)
    {
        this.gunSystem.TryingToShoot = false;
        if (state.StateName == shooting.name)
        {
            this.gunSystem.TryingToShoot = true;
        } 
    }
}
