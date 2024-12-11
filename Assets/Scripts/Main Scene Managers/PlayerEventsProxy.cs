using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class PlayerEventsProxy : MonoBehaviour
{
    //Setup Dependencies
    [Header("Setup Dependencies")]
    [SerializeField] private SignalAsset playerDeathVoicelinesSignal;
    public void PlayerSpawned(GameObject player)
    {
        var components = player.GetComponent<UsefulPlayerComponents>();
        //Register to events on the player
        //when the player spawns they will register themselves to our events
        
        //hook up to PlayerDeathVoicelines
        components.SignalReceiver.GetReaction(this.playerDeathVoicelinesSignal).AddListener(PlayerDeathVoiceline);
    }
    
    

    //From Player Out
    [Header("From Player Out")]
    [SerializeField] private string PlayerDeathVoicelineString;
    public UnityEvent<string> PlayerDeathVoicelineEvent;
    public void PlayerDeathVoiceline()
    {
        this.PlayerDeathVoicelineEvent?.Invoke(this.PlayerDeathVoicelineString);
    }
    
    

    
    
    
    //From outside into player
    [Header("From outside into player")]
    [HideInInspector] public UnityEvent EnemyDeathEventVoid;
    [HideInInspector] public UnityEvent<EnemyBase> EnemyDeathEvent;
    public void EnemyDeath(EnemyBase enemy)
    {
        this.EnemyDeathEventVoid?.Invoke();
        this.EnemyDeathEvent?.Invoke(enemy);
    }


    [HideInInspector]
    public UnityEvent<string> EventUserSubmittedSoundLoaderStatusUpdateEvent; 
    public void UserSubmittedSoundLoaderStatusUpdate(string status)
    {
        this.EventUserSubmittedSoundLoaderStatusUpdateEvent?.Invoke(status);
    }


    [HideInInspector] public UnityEvent<FloatLerpPackage> PlayerOutOfBoundsEvent;
    public void PlayerOutOfBounds(FloatLerpPackage package)
    {
        this.PlayerOutOfBoundsEvent?.Invoke(package);
    }

    [HideInInspector] public UnityEvent PlayerReturnedToBoundsEvent;

    public void PlayerReturnedToBounds()
    {
        this.PlayerReturnedToBoundsEvent?.Invoke();
    }
}
