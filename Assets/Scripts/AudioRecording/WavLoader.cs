using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public static class WavLoader 
{
    public static AudioClip LoadClip(string filepath)
    {
        //creating an audio clip from wav bytes credit gpt
        byte[] fileData = File.ReadAllBytes(filepath);

        int headerSize = 44;

        int dataSize = fileData.Length - headerSize;
        
        //extract audio data
        float[] audioData = new float[dataSize / 2];
        for (int i = 0, j = headerSize; i < audioData.Length; i++, j += 2)
        {
            audioData[i] = ((short)((fileData[j + 1] << 8) | fileData[j])) / 32768f; //normalize to -1 to 1 range
        }

        AudioClip clip = AudioClip.Create("Loaded Wav", audioData.Length, 1, 44100, false);
        clip.SetData(audioData, 0);
        return clip;
    }
}
