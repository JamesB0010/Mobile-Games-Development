using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class WavLoader 
{
    public static AudioClip LoadClip(string filepath)
    {
        #if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
        return Resources.Load<AudioClip>(filepath);
    }
}
