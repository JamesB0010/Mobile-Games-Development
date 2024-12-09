using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesPlayedIncrementor : MonoBehaviour
{
    [SerializeField] private IntReference gamesPlayed;


    private void Start()
    {
        int prevGamesPlayed = this.gamesPlayed.GetValue();
        this.gamesPlayed.SetValue(prevGamesPlayed + 1);
        
        #if UNITY_EDITOR
        #else
        foreach()
        #endif
    }
}
