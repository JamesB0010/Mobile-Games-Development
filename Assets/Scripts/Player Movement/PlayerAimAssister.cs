using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAssister : MonoBehaviour
{
    [SerializeField] private BoolReference zooming;
    [SerializeField] private BoolReference aimingAtEnemy;

    [SerializeField] private PlayerShipAilerons ailerons;

    [SerializeField] private PlayerShipElevator elavator;

    [SerializeField] private PlayerShipRudder rudder;

    private void Start()
    {
        aimingAtEnemy.valueChanged += this.OnAimingAtEnemyChanged; 
    }

    private void OnAimingAtEnemyChanged(bool aimingAtEnemy)
    {
        if (this.zooming.GetValue())
            return;
        
        if (this.aimingAtEnemy.GetValue() == true)
        {
            this.ailerons.SetSensitivityHover();
            this.elavator.SetSensitivityHover();
            this.rudder.SetSensitivityHover();
        }
        else
        {
            this.ailerons.SetSensitivityNormal();
            this.elavator.SetSensitivityNormal();
            this.rudder.SetSensitivityNormal();
        }
    }
}
