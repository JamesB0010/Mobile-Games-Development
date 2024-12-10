using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUiInitializer : MonoBehaviour
{
    [SerializeField] private BoolReference gyroEnabled;

    [SerializeField] private ToggleSwitch gyroToggleSwitch;

    private void Start()
    {
        this.gyroToggleSwitch.CurrentValue = this.gyroEnabled.GetValue();
    }
}
