using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUiInitializer : MonoBehaviour
{
    [SerializeField] private ToggleSwitch gyroToggleSwitch;

    [SerializeField] private ToggleSwitch pitchInvertedToggleSwitch;

    [SerializeField] private ToggleSwitch simpleControls;

    private void Start()
    {
        this.gyroToggleSwitch.CurrentValue = BuzzardGameData.GyroEnabled.GetValue();

        this.pitchInvertedToggleSwitch.CurrentValue = BuzzardGameData.PitchInverted.GetValue();

        this.simpleControls.CurrentValue = BuzzardGameData.SimpleControls.GetValue();
    }
}
