using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class VoiceActivatedShip : MonoBehaviour
{
    [SerializeField] private AudioClip[] voiceLines;

    private AudioSource audioSource;
    private bool listenForStopPlaying;

    private int nextVoiceline = 0;

    [SerializeField] private Button playerVoicelineButton;

    private int completedVoicelines = 0;

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.playerVoicelineButton.interactable = false;
    }

    public void FinishedEnteringShip()
    {
        this.PlayQueuedVoiceline();
    }

    private void PlayQueuedVoiceline()
    {
        if (this.completedVoicelines == 3)
        {
            this.AllVoicelinesComplete();
            return;
        }
        
        this.audioSource.clip = this.voiceLines[this.nextVoiceline];
        this.audioSource.Play();
        this.listenForStopPlaying = true;       
    }

    private void Update()
    {
        if(this.listenForStopPlaying)
            if (this.audioSource.isPlaying == false)
                this.VoiceLineFinished();
    }

    private void VoiceLineFinished()
    {
        this.listenForStopPlaying = false;
        this.playerVoicelineButton.interactable = true;
        this.nextVoiceline++;
        this.nextVoiceline %= this.voiceLines.Length;

        this.completedVoicelines++;
        
        if (this.nextVoiceline == 2)
        {
            this.playerVoicelineButton.interactable = false;
            this.StartCoroutine(nameof(this.WaitBeforeNextVoiceline), 2.5f);
        }
    }

    IEnumerator WaitBeforeNextVoiceline(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        
        this.PlayQueuedVoiceline();
    }

    public void PlayerSaidVoiceline()
    {
        this.playerVoicelineButton.interactable = false;
        this.PlayQueuedVoiceline();
    }

    public void AllVoicelinesComplete()
    {
        GetComponent<PlayableDirector>().Play(); 
        this.playerVoicelineButton.gameObject.SetActive(false);
    }
}
