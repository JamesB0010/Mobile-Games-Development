using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

//credits https://www.youtube.com/watch?v=-ADNOjEs1pA&t=475s

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    private string filePath = "recording.wav";
    private string directoryPath = Application.dataPath + "/Resources/AudioRecordings";
    private float startTime;
    private float recordingLength;
    private bool recording = false;


    [SerializeField] private UnityEvent<string> AudioRecorderStatusUpdate = new UnityEvent<string>();


    public void ToggleRecording()
    {
        if (this.recording)
        {
            this.StopRecording();
        }
        else
        {
            this.StartRecording();
        }
    }

    public void StartRecording()
    {
        this.AudioRecorderStatusUpdate?.Invoke("Recording");
        this.recording = true;
        string device = Microphone.devices[0];
        const int sampleRate = 44100;
        const int lengthSec = 3599;

        this.recordedClip = Microphone.Start(device, false, lengthSec, sampleRate);
        startTime = Time.realtimeSinceStartup;
    }

    public void StopRecording()
    {
        this.AudioRecorderStatusUpdate?.Invoke("Recording Finished");
        this.recording = false;
        Microphone.End(null);
        recordingLength = Time.realtimeSinceStartup - this.startTime;
        recordedClip = this.TrimClip(this.recordedClip, this.recordingLength);
        this.SaveRecording();
    }

    private AudioClip TrimClip(AudioClip clip, float length)
    {
        int samples = (int)(clip.frequency * length);
        float[] data = new float[samples];
        clip.GetData(data, 0);

        AudioClip trimmedClip = AudioClip.Create(clip.name, samples, clip.channels, clip.frequency, false);
        trimmedClip.SetData(data, 0);

        return trimmedClip;
    }

    private void SaveRecording()
    {
        if(recordedClip != null)
        {
            this.filePath = Path.Combine(this.directoryPath, this.filePath);
            WavUtility.Save(filePath, this.recordedClip);
            Debug.Log($"Recording saved as {filePath}");
        }
        else
        {
            Debug.Log("No recording found to save");
        }
        
    }
}
