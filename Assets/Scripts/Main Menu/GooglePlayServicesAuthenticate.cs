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
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //continue with play games services
            Debug.Log("Sign in Successful");
        }
        else
        {
            Debug.Log("auto sign in didnt work maybe try manually auth");
        }
    }
}
