using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAudioSounds : ScriptableObject
{
    [SerializeField] private AudioClip defaultClickSound;
    [SerializeField] private AudioClip fuzzyClickSound;
    [SerializeField] private AudioClip toggleOffSound;
    [SerializeField] private AudioClip toggleOnSound;


    private AudioSource audioPlayer = null;

    private AudioSource AudioPlayer
    {
        get
        {
            this.audioPlayer = audioPlayer != null ? this.audioPlayer : new GameObject("Ui sounds").AddComponent<AudioSource>();
            this.audioPlayer.playOnAwake = false;
            this.audioPlayer.gameObject.hideFlags = HideFlags.HideAndDontSave;
            return this.audioPlayer;
        }
    }


    public void PlayDefaultClickSound()
    {
        this.PlayClip(this.defaultClickSound);
    }

    public void PlayFuzzyClickSound()
    {
        this.PlayClip(this.fuzzyClickSound);
    }
    
    public void PlayToggleOnSound()
    {
        this.PlayClip(this.toggleOnSound);
    }

    public void PlayToggleOffSound()
    {
        this.PlayClip(this.toggleOffSound);
    }

    public void PlayClip(AudioClip clip)
    {
        this.AudioPlayer.Stop();
        this.AudioPlayer.clip = clip;
        this.AudioPlayer.Play();
    }
}
