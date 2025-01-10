using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PitchAndRollInitalizer : MonoBehaviour
{
    [SerializeField] private BoolReference gyroEnabled;

    private void Awake()
    {
        if(this.gyroEnabled.GetValue())
            base.gameObject.SetActive(false);
    }
}
