using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseSettingsButton : MonoBehaviour
{
    private Button button;

    [SerializeField] private PlayableDirector settingsUi;

    [SerializeField] private TimelineAsset closeSettings;

    [SerializeField] private UnityEvent settingsClosed;

    private void Awake()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(this.OnClick); 
    }

    private void OnClick()
    {
        this.settingsUi.playableAsset = this.closeSettings;
        this.settingsUi.Play();
        this.settingsUi.stopped+= this.DisableSettingsScreen;
    }

    private void DisableSettingsScreen(PlayableDirector obj)
    {
        this.settingsClosed?.Invoke();
        this.settingsUi.stopped-= this.DisableSettingsScreen;
    }
}