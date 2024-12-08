using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class UserSubmittedSoundLoader : MonoBehaviour
{
    private AudioClip clip;
    [SerializeField] private AudioClip defaultAudioClip;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private UnityEvent<string> TestAudioPlayerStatusUpdate = new UnityEvent<string>();

    public void LoadClip(string filepath)
    {
        if (File.Exists(Application.dataPath + "/Resources/" + filepath + ".wav"))
        {
            clip = WavLoader.LoadClip(filepath);
            this.audioSource.clip = clip;
            this.TestAudioPlayerStatusUpdate?.Invoke("Clip Loaded");
        }
        else
        {
            this.audioSource.clip = this.defaultAudioClip;
            this.TestAudioPlayerStatusUpdate?.Invoke("Default clip Loaded");
        }
    }

    public void LoadAndPlayClip(string filepath)
    {
        this.LoadClip(filepath);
        this.audioSource.Play();
    }
}
