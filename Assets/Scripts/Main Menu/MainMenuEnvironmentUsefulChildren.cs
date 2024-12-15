using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainMenuEnvironmentUsefulChildren : MonoBehaviour
{
    [SerializeField]
    private List<KeyValuePairWrapper<string, GameObject>> part1SignalTrackToReference = new ();

    public List<KeyValuePairWrapper<string, GameObject>> Part1SignalTrackToReference => this.part1SignalTrackToReference;


    [SerializeField] private List<KeyValuePairWrapper<string, GameObject>> part2SignalTrackToReference = new();

    public List<KeyValuePairWrapper<string, GameObject>> Part2SignalTrackToReference => this.part2SignalTrackToReference;
}
