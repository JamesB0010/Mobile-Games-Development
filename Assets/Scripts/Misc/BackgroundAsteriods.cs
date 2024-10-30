using System;
using System.Collections;
using System.Collections.Generic;
using Player_Movement;
using UnityEngine;

public class BackgroundAsteriods : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;

    private void Start()
    {
        this.offset = player.position - transform.position;
    }

    private void Update()
    {
        transform.position = player.position + this.offset;
    }
}
