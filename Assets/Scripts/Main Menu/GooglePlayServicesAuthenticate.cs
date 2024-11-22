using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayServicesAuthenticate : MonoBehaviour
{
    private void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(this.ProcessAutoAuthentication);
    }

    private void ProcessAutoAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            this.OnSuccessfulSignIn();
        }
        else
        {
            Debug.Log("auto sign in didnt work maybe try manually auth");
            PlayGamesPlatform.Instance.ManuallyAuthenticate(this.ProcessAutoAuthentication);
        }
    }

    private void ProcessManualAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            this.OnSuccessfulSignIn();
        }
        else
        {
            Debug.Log("Tried manual and it didnt work... cooked");
        }
    }

    private void OnSuccessfulSignIn()
    {
            Debug.Log("Sign in Successful");
    }
}
