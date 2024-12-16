using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//credit on how to use huggingface api and get user audio https://www.youtube.com/watch?v=Ngmb7l7tO0I

[RequireComponent(typeof(Button))]
public class VoiceRecordingButton : MonoBehaviour, IPointerDownHandler
{
    private Button button;

    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private VoiceActivatedShip voiceActivatedShip;


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
        //this.voiceActivatedShip.PlayerSaidVoiceline();
    }

    private void SendRecording()
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(this.bytes, response =>
        {
            this.outputText.text = response;
        }, error =>
        {
            this.outputText.text = error;
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
