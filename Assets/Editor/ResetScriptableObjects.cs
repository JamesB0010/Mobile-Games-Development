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

    private static ResetScriptableObjects instance = null;
    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
            return;
        }
        
        ScriptableObject.Destroy(this);
    }

    private void OnEnable()
    {
         if (instance == null && instance != this)
         {
             instance = this;
             return;
         }
         
         ScriptableObject.DestroyImmediate(this);       
    }

    //This resets the scriptable objects which need to be resetted for the game to be built
    public static void WipeSaveData()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, "Json");
        string audioPath = Path.Combine(Application.persistentDataPath, "AudioRecordings"); 
        
        if(Directory.Exists(jsonPath))
            Directory.Delete(jsonPath, true);
        
        if(Directory.Exists(audioPath))
            Directory.Delete(audioPath, true);
        
        instance.upgradesState.ResetAll();
        
        Debug.Log("Persistant data wiped");
    }
}