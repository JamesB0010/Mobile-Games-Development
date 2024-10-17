using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Playerenginesoundsmanager : MonoBehaviour
{
    //Attributes
    private PlayerMovement playerMovement;
    
    //Injected Dependencies
    [SerializeField]
    private AudioSource bigThruster, littleThruster;


    [Header("Configurables")]
    
    [Header("Big Thruster")]
    [FormerlySerializedAs("minVol")] [SerializeField] private float bigThursterMinVol;
    [FormerlySerializedAs("maxVol")] [SerializeField] private float bigThrusterMaxVol;

    [Header("Big Thruster Boosting")]
    [SerializeField] private float bigThrusterBoostingMinVol;
    [SerializeField] private float bigThrusterBoostingMaxVol;


    [Header("Little Thruster")] 
    [SerializeField] private float littleThrusterMinVol; 
    [SerializeField] private float littleThrusterMaxVol;

    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        SetBigThrusterVolume();
        this.littleThruster.volume = ValueInRangeMapper.Map(this.playerMovement.Throttle, 0, 1,
            this.littleThrusterMinVol, this.littleThrusterMaxVol);
    }

    private void SetBigThrusterVolume()
    {
        if (this.playerMovement.isBoosting)
        {
            this.bigThruster.volume = ValueInRangeMapper.Map(this.playerMovement.Throttle, 0, 1,
                this.bigThrusterBoostingMinVol, this.bigThrusterBoostingMaxVol);
        }
        else
        {
            this.bigThruster.volume = ValueInRangeMapper.Map(this.playerMovement.Throttle, 0, 1, this.bigThursterMinVol,
                this.bigThrusterMaxVol);
        }
    }
}
