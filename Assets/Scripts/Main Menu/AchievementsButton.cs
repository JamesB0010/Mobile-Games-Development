using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class AchievementsButton : MonoBehaviour 
{
    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
    }
}
