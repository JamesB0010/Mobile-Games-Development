using System.Collections;
using System.Collections.Generic;
using Player_Movement;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private Vector3 distancetoMaintain;

    private Transform playerTransform;

    public void OnPlayerSpawned(GameObject player)
    {
        //get moving part
        Transform movingPart = player.transform.GetChild(0);
        this.playerTransform = movingPart;
        
        this.distancetoMaintain = transform.position - this.playerTransform.position;

        this.enabled = true;
    }

    void Update()
    {
        transform.position = this.playerTransform.position + this.distancetoMaintain;
    }
}
