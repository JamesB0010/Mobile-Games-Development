using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector startGameDirector, part1IntroDirector;

    [SerializeField] private MainMenuUi mainMenuUi;

    private bool readyToStart = false;

    private void Start()
    {
        LoadingScreenSceneIndexCounter.NextSceneIndex = 0;
        BuzzardGameData.ReadSaveGame();
    }

    public void SetReadyToStart(bool value)
    {
        this.readyToStart = value;
    }
    
    public void EnterGameButtonPressed()
    {
        #if UNITY_EDITOR
        part1IntroDirector.Play();
        mainMenuUi.HideUI();
        return;
        #endif
        
        if (!this.readyToStart)
            return;
        part1IntroDirector.Play();
        
        mainMenuUi.HideUI();
        
    }
    public void IntroFirstPartComplete()
    {
        this.startGameDirector.Play();
    }

}
