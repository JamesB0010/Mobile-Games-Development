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

    [SerializeField] private UnityEvent SignInSucessEvent = new UnityEvent();

    [SerializeField] private UnityEvent SignInFailedEvent = new UnityEvent();
    
    

    private void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(this.ProcessAuthentication);
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //signed in
            this.SignInSucess();
        }
        else
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(this.ProcessManualAuthentication);
        }
    }

    private void SignInSucess()
    {
        this.SignInSucessEvent?.Invoke();
    }

    private void ProcessManualAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //yay
            this.SignInSucess();
        }
        else
        {
            //cooked
            this.SignInFailedEvent?.Invoke();
        }
    }

    public void EnterGameButtonPressed()
    {
        part1IntroDirector.Play();
        
    }
    public void IntroFirstPartComplete()
    {
        this.startGameDirector.Play();
    }

}
