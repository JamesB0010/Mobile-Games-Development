using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillCounter : MonoBehaviour
{
    [Serializable]
    private class KillAchievement
    {
        [SerializeField] private int killsRequired;
        public int KillsRequired => this.killsRequired;

        [SerializeField] private string achievementId;
        public string AchievementId => this.achievementId;
    }
    
    [SerializeField] private IntReference playerKills;

    [SerializeField] private KillAchievement[] killAchievements;
    
    public void OnEnemyKilled()
    {
        int newKillCount = this.playerKills.GetValue() + 1;
        this.playerKills.SetValue(newKillCount);
        
        //Achievements
#if UNITY_EDITOR
        foreach (var achievement in killAchievements)
        {
            if (newKillCount == achievement.KillsRequired)
            {
                Social.ReportProgress(achievement.AchievementId, 100.0f, sucess =>
                {
                    
                });
            }
        }
        #else
        
#endif
    }
}
