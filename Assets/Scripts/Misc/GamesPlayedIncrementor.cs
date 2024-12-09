using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesPlayedIncrementor : MonoBehaviour
{
    [SerializeField] private IntReference gamesPlayed;

    [SerializeField] private KeyValuePairWrapper<string, int>[] gamesPlayedAchievements;
    
    private void Start()
    {
        int newKillCount = this.gamesPlayed.GetValue() + 1;
        this.gamesPlayed.SetValue(newKillCount);

        #if UNITY_EDITOR
        #else
        foreach (var achievement in this.gamesPlayedAchievements)
        {
            if (newKillCount == achievement.value)
            {
                Social.ReportProgress(achievement.key, 100.0f, sucess =>
                {
                    
                });
            }
        }
        #endif
    }
}
