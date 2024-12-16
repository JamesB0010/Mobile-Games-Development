using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HuggingFace.API;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//credit on how to use huggingface api and get user audio https://www.youtube.com/watch?v=Ngmb7l7tO0I

[RequireComponent(typeof(Button))]
public class VoiceRecordingButton : MonoBehaviour, IPointerDownHandler
{
    private Button button;

    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private VoiceActivatedShip voiceActivatedShip;


    [SerializeField] private UnityEvent<string> UserSpeechInterpreted;

    [SerializeField] private string[] similarEnoughWordsAdAstra;

    [SerializeField] private string[] similarEnoughWordsBuzzard;


    private AudioClip playerRecordedAudio;

    private byte[] bytes;

    private bool recording = false;

    private void Awake()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(this.OnReleased);
    }

    private void OnPressed()
    {
        Debug.Log("Pressed Down");
        this.playerRecordedAudio = Microphone.Start(null, false, 10, 44100);
        this.recording = true;
    }
    
    private void OnReleased()
    {
        Debug.Log("Released");

        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * this.playerRecordedAudio.channels];
        this.playerRecordedAudio.GetData(samples, 0);
        this.bytes = this.EncodeAsWAV(samples, this.playerRecordedAudio.frequency, playerRecordedAudio.channels);
        this.SendRecording();
        this.recording = false;
        
        this.outputText.text = "";
    }

    private void SendRecording()
    {
        this.button.interactable = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(this.bytes, response =>
        {
            response = response.ToLower();
            response = response.Trim();
            response = response.TrimEnd('.');
            response = response.TrimEnd('?');

            bool similarEnough = false;

            for (int i = 0; i < this.similarEnoughWordsBuzzard.Length; i++)
            {
                similarEnough = similarEnough || response == this.similarEnoughWordsBuzzard[i];
                if (similarEnough == true)
                    break;
            }

            if (similarEnough)
            {
                this.UserSpeechInterpreted?.Invoke("buzzard");
                return;
            }

            similarEnough = false;

            for (int i = 0; i < this.similarEnoughWordsAdAstra.Length; i++)
            {
                similarEnough = similarEnough || response == this.similarEnoughWordsAdAstra[i];
                if (similarEnough == true)
                    break;
            }

            similarEnough = response.Contains("ad astra house") || response.Contains("add astra house") || response.Contains("at astra house") || response.Contains("at astro house") || response.Contains("add astro house") || response.Contains("ad astro house") || response.Contains("ad astrahouse condol") || response.Contains("astrahouse");

            if (similarEnough)
            {
                this.UserSpeechInterpreted?.Invoke("ad astra house condor");
                return;
            }
            
            
            this.UserSpeechInterpreted?.Invoke(response);
            this.button.interactable = true;
        }, error =>
        {
            this.button.interactable = true;
        });
    }

    private void Update()
    {
        if(this.recording && Microphone.GetPosition(null) >= this.playerRecordedAudio.samples)
            this.OnReleased();
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
            using (var writer = new BinaryWriter(memoryStream)) {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples) {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.OnPressed();
    }
}
