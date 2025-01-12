using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ResetScriptableObjects : ScriptableObject
{
    [SerializeField] private PlayerUpgradesState upgradesState;

    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void WipeSaveData()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, "Json");
        string audioPath = Path.Combine(Application.persistentDataPath, "AudioRecordings"); 
        
        if(Directory.Exists(jsonPath))
            Directory.Delete(jsonPath, true);
        
        if(Directory.Exists(audioPath))
            Directory.Delete(audioPath, true);
        
        Debug.Log("Persistant data wiped");
    }
}