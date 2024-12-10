using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAndPitchJoystick : MonoBehaviour
{
    [SerializeField] private BoolReference gyroControls;

    private void Start()
    {
        if (this.gyroControls.GetValue())
        {
            this.gameObject.SetActive(false);
        }
    }
}
