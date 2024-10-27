using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerEngineSoundsManager : MonoBehaviour
    {
        //Attributes
        private PlayerShipThrottle playerThrottle;

        private PlayerShipBooster booster;

        //Injected Dependencies
        [SerializeField] private AudioSource bigThruster, littleThruster;


        [Header("Configurables")] [Header("Big Thruster")] [FormerlySerializedAs("minVol")] [SerializeField]
        private float bigThursterMinVol;

        [FormerlySerializedAs("maxVol")] [SerializeField]
        private float bigThrusterMaxVol;

        [Header("Big Thruster Boosting")] [SerializeField]
        private float bigThrusterBoostingMinVol;

        [SerializeField] private float bigThrusterBoostingMaxVol;


        [Header("Little Thruster")] [SerializeField]
        private float littleThrusterMinVol;

        [SerializeField] private float littleThrusterMaxVol;

        private void Start()
        {
            this.playerThrottle = FindObjectOfType<PlayerShipThrottle>();
            this.booster = FindObjectOfType<PlayerShipBooster>();
        }

        private void Update()
        {
            SetBigThrusterVolume();
            this.littleThruster.volume =
                this.playerThrottle.Throttle.MapRange(0, 1, this.littleThrusterMinVol, this.littleThrusterMaxVol);
        }

        private void SetBigThrusterVolume()
        {
            if (this.booster.IsBoosting)
            {
                this.bigThruster.volume = this.playerThrottle.Throttle.MapRange(0, 1, this.bigThrusterBoostingMinVol,
                    this.bigThrusterBoostingMaxVol);
            }
            else
            {
                this.bigThruster.volume =
                    this.playerThrottle.Throttle.MapRange(0, 1, this.bigThursterMinVol, this.bigThrusterMaxVol);
            }
        }
    }
}