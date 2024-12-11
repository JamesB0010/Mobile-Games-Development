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

    [SerializeField] private SceneChanger sceneChanger;
    
    private bool readyToStart = false;

    private void Start()
    {
        LoadingScreenSceneIndexCounter.NextSceneIndex = 2;
        BuzzardGameData.ReadSaveGame();
    }

    public void SetReadyToStart(bool value)
    {
        this.readyToStart = value;
    }
    
    public void EnterGameButtonPressed()
    {
        #if UNITY_EDITOR
        this.sceneChanger.LoadSceneWithLoadingScreen(2);
        part1IntroDirector.Play();
        mainMenuUi.HideUI();
        return;
        #endif
        
        if (!this.readyToStart)
            return;
        
        
        this.sceneChanger.LoadSceneWithLoadingScreen(2);
        part1IntroDirector.Play();
        
        mainMenuUi.HideUI();
        
    }
    public void IntroFirstPartComplete()
    {
        this.startGameDirector.Play();
    }

}
