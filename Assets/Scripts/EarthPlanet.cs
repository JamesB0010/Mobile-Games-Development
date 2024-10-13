using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPlanet : MonoBehaviour
{
    private PlayerMovement player;

    private Vector3 distanceToStayAwayFromPlayer;
    void Start()
    {
        this.player = FindObjectOfType<PlayerMovement>();
        this.distanceToStayAwayFromPlayer = transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.player.transform.position + this.distanceToStayAwayFromPlayer;
    }
}
