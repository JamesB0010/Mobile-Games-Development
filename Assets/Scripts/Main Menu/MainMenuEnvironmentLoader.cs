using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

public class MainMenuEnvironmentLoader : MonoBehaviour
{
    [SerializeField] private AssetReference environemnt;

    [SerializeField] private PlayableDirector introSequencePart1, introSequencePart2;

    private void Awake()
    {
        environemnt.InstantiateAsync().Completed += this.EnvironmentLoaded;
    }

    private void EnvironmentLoaded(AsyncOperationHandle<GameObject> obj)
    {
        SetupTimelineDependencies(obj);
    }


    private void SetupTimelineDependencies(AsyncOperationHandle<GameObject> obj)
    {
        var dependencies = obj.Result.GetComponent<MainMenuEnvironmentUsefulChildren>();

        List<KeyValuePairWrapper<string, GameObject>> introSequence1Dependencies =
            dependencies.Part1SignalTrackToReference;

        List<KeyValuePairWrapper<string, GameObject>> introSequence2Dependencies =
            dependencies.Part2SignalTrackToReference;

        //Setup the dependencies
        TimelineAsset timeline = (TimelineAsset)this.introSequencePart1.playableAsset;
        TimelineAsset timeline2 = (TimelineAsset)this.introSequencePart2.playableAsset;
        
        this.SetupTimelineDependencies(introSequence1Dependencies, timeline, this.introSequencePart1);
        this.SetupTimelineDependencies(introSequence2Dependencies, timeline2, this.introSequencePart2);
    }
    private void SetupTimelineDependencies(List<KeyValuePairWrapper<string, GameObject>> sequenceDependencies,
        TimelineAsset timeline, PlayableDirector sequence)
    {
        foreach (TrackAsset track in timeline.GetOutputTracks())
        {
            sequenceDependencies.ForEach(dependency =>
            {
                if (track.name == dependency.key)
                {
                    if (track.name.Contains("Signal"))
                    {
                        sequence.SetGenericBinding(track, dependency.value.GetComponent<SignalReceiver>());
                    }
                    else if (track.name.Contains("Animation"))
                    {
                        sequence.SetGenericBinding(track, dependency.value.GetComponent<Animator>());
                    }
                    else
                    {
                        sequence.SetGenericBinding(track, dependency.value);
                    }
                }
            });
        }
    }
}
