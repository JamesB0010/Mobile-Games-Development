using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Playerenginesoundsmanager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bigThruster, littleThruster;

    private PlayerMovement playerMovement;

    [FormerlySerializedAs("minVol")] [SerializeField] private float bigThursterMinVol;
    [FormerlySerializedAs("maxVol")] [SerializeField] private float bigThrusterMaxVol;


    [SerializeField] private float littleThrusterMinVol, littleThrusterMaxVol;

    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        this.bigThruster.volume = ValueInRangeMapper.Map(this.playerMovement.Throttle, 0, 1, this.bigThursterMinVol, this.bigThrusterMaxVol);
        this.littleThruster.volume = ValueInRangeMapper.Map(this.playerMovement.Throttle, 0, 1,
            this.littleThrusterMinVol, this.littleThrusterMaxVol);
    }
}
