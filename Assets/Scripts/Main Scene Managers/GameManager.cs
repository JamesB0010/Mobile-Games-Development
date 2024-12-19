using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Transform player;

    public Transform Player => this.player;

    
    public void OnPlayerSpawned(GameObject player)
    {
        //get the moving part of the player
        Transform movingPart = player.transform.GetChild(0);
        this.player = movingPart;
    }
}
