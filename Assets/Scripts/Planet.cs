using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private Vector3 distancetoMaintain;

    private Transform playerTransform;
    void Start()
    {
        this.playerTransform = FindObjectOfType<PlayerMovement>().transform;
        this.distancetoMaintain = transform.position - this.playerTransform.position;
    }

    void Update()
    {
        transform.position = this.playerTransform.position + this.distancetoMaintain;
    }
}
