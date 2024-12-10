using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class SettingsButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayableDirector settingsUi;

    [SerializeField] private TimelineAsset openSettings;

    [SerializeField] private UnityEvent settingsOpened = new UnityEvent();
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        this.settingsOpened?.Invoke();
        this.settingsUi.gameObject.SetActive(true);
        this.settingsUi.playableAsset = this.openSettings;
        this.settingsUi.Play();
    }
}
