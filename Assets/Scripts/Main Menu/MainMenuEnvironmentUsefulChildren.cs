using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuEnvironmentUsefulChildren : MonoBehaviour
{
    [FormerlySerializedAs("part1SignalTrackToReference")] [SerializeField]
    private List<KeyValuePairWrapper<string, GameObject>> enterShipSgnalTrackToReference = new ();

    public List<KeyValuePairWrapper<string, GameObject>> EnterShipSgnalTrackToReference => this.enterShipSgnalTrackToReference;


    [SerializeField] private List<KeyValuePairWrapper<string, GameObject>> part2SignalTrackToReference = new();

    public List<KeyValuePairWrapper<string, GameObject>> Part2SignalTrackToReference => this.part2SignalTrackToReference;
}
