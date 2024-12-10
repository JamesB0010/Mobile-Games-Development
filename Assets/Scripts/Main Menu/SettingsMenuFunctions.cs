using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuFunctions : ScriptableObject
{
    [SerializeField] private BoolReference gyroEnabled;
    public void OnGroToggled(bool newState)
    {
        this.gyroEnabled.SetValue(newState);
    }
}
