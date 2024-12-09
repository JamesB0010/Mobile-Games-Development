using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerKillCounter : MonoBehaviour
{
    
    [SerializeField] private IntReference playerKills;
    
    [SerializeField] private KeyValuePairWrapper<string, int>[] killAchievements;

    public void OnEnemyKilled()
    {
        int newKillCount = this.playerKills.GetValue() + 1;
        this.playerKills.SetValue(newKillCount);

        //Achievements
#if UNITY_EDITOR
#else
        foreach (var achievement in killAchievements)
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
