using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class VoiceActivatedShip : MonoBehaviour
{
    [SerializeField] private AudioClip[] voiceLines;

    [SerializeField] private string[] voiceLinesText;

    [SerializeField] private TextMeshProUGUI subtitlesText;

    [SerializeField] private TextMeshProUGUI playerInterpretedText;
    
    private AudioSource audioSource;
    private bool listenForStopPlaying;

    private int nextVoiceline = 0;

    private int currentVoicelineText = 0;

    [SerializeField] private Button playerVoicelineButton;

    private int completedVoicelines = 0;
    private string targetWord = "buzzard";

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.playerVoicelineButton.interactable = false;
    }

    public void FinishedEnteringShip()
    {
        SetCinemachineBrainBlendTime();

        this.PlayQueuedVoiceline();
        StartCoroutine(nameof(this.EnableSubtitlesAfter), 0.65f);
        StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 2.1f);
        StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 4.15f);
        StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 7f);
        StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 7.5f);
    }

    private static void SetCinemachineBrainBlendTime()
    {
        var cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        
    }

    IEnumerator EnableSubtitlesAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.subtitlesText.gameObject.SetActive(true);
    }

    IEnumerator AdvanceSubtitlesAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AdvanceSubtitles();
    }

    IEnumerator PlayQueuedVoicelineAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.PlayQueuedVoiceline();
    }
    private IEnumerator AllVoicelinesCompleteAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.AllVoicelinesComplete();
    }

    private void AdvanceSubtitles()
    {
        this.currentVoicelineText++;
        this.subtitlesText.text = this.voiceLinesText[this.currentVoicelineText];
        this.playerInterpretedText.text = "";
        this.playerInterpretedText.color = Color.white;
    }

    
    private void PlayQueuedVoiceline()
    {
        if (this.completedVoicelines == 3)
        {
            StartCoroutine(nameof(this.AllVoicelinesCompleteAfter), 0.5f);
            return;
        }
        
        this.audioSource.clip = this.voiceLines[this.nextVoiceline];
        this.audioSource.Play();
        this.listenForStopPlaying = true;

        if (this.nextVoiceline == 1)
        {
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 0.5f);
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 1.2f);
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 4f);
        }
        else if (this.nextVoiceline == 2)
        {
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 0.4f);
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 3.2f);
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 5.6f);
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 8.6f);
            StartCoroutine(nameof(this.AdvanceSubtitlesAfter), 9f);
            this.targetWord = "ad astra house condor";
        }
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

    public void EvaluateIfPlayerSaidCorrectWords(string playerWords)
    {
        if(playerWords == this.targetWord)
            this.PlayerSaidVoiceline();
            
    }
    
    private void PlayerSaidVoiceline()
    {
        this.playerVoicelineButton.interactable = false;
        this.playerInterpretedText.color = Color.green;
        StartCoroutine(nameof(this.PlayQueuedVoicelineAfter), 0.4f);
    }

    public void AllVoicelinesComplete()
    {
        GetComponent<PlayableDirector>().Play(); 
        this.playerVoicelineButton.gameObject.SetActive(false);
        this.playerInterpretedText.text = "";
        this.subtitlesText.gameObject.SetActive(false);
    }
}
