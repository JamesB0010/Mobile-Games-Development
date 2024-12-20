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
    [SerializeField] private PlayableDirector enterShip, startGameDirector, part1IntroDirector;

    [SerializeField] private MainMenuUi mainMenuUi;

    [SerializeField] private SceneChanger sceneChanger;
    
    private bool readyToStart = false;

    [SerializeField] private UnityEvent startGameAttemptFailed;

    private int modelsLoaded = 0;

    [SerializeField] private int loadedModelsRequired = 2;

    private Ping checkInternet;

    private bool connectedToInternet = false;

    private void Start()
    {
        LoadingScreenSceneIndexCounter.NextSceneIndex = 2;

        this.checkInternet = new Ping("8.8.8.8");
    }

    private void Update()
    {
        if (this.checkInternet.isDone)
            this.connectedToInternet = true;
    }

    public void AuthenticatedSucessfully()
    {
        this.readyToStart = true;
        BuzzardGameData.ReadSaveGame();
    }

    public void EnterGameButtonPressed()
    {
        #if UNITY_EDITOR
        #else
        if (!this.readyToStart)
        {
            this.startGameAttemptFailed?.Invoke();
            return;
        }
        #endif

        if (this.modelsLoaded < this.loadedModelsRequired || !this.connectedToInternet)
            return;


        this.EnterShip();
        
        mainMenuUi.HideUI();
    }

    public void IncrementModelsLoaded()
    {
        this.modelsLoaded++;
    }

    public void StartTakeoffSequence()
    {
        part1IntroDirector.Play();
    }

    private void EnterShip()
    {
        this.enterShip.Play();
    }


    public void IntroFirstPartComplete()
    {
        this.startGameDirector.Play();
    }

}
