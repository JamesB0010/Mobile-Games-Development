using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class VoiceActivatedShip : MonoBehaviour
{
    public void OnVoiceLineComplete()
    {
        GetComponent<PlayableDirector>().Play(); 
    }
}
