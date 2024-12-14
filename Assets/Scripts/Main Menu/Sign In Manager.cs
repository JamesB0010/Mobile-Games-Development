using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SignInManager : MonoBehaviour
{
    [SerializeField] private UnityEvent SignInSucessEvent = new UnityEvent();
    
    [SerializeField] private UnityEvent SignInFailedEvent = new UnityEvent();

    private void Awake()
    {
        PlayGamesPlatform.Activate();
    }

    private void Start()
     {
         PlayGamesPlatform.Instance.Authenticate(this.ProcessAuthentication);
     }

    public void ManuallyAuthenticate()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(this.ProcessManualAuthentication);
    }
 
     private void ProcessAuthentication(SignInStatus status)
     {
         if (status == SignInStatus.Success)
         {
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
             this.SignInSucess();
         }
         else
         {
             this.SignInFailedEvent?.Invoke();
         }
     }   
}
