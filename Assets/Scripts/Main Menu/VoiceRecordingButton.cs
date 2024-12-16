using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VoiceRecordingButton : MonoBehaviour, IPointerDownHandler
{
    private Button button;

    [SerializeField] private VoiceActivatedShip voiceActivatedShip;

    private void Awake()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(this.OnReleased);
    }

    private void OnPressed()
    {
        Debug.Log("Pressed Down");
    }
    
    private void OnReleased()
    {
        Debug.Log("Released");
        this.voiceActivatedShip.PlayerSaidVoiceline();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.OnPressed();
    }
}
