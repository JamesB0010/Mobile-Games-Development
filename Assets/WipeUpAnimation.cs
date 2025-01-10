using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class WipeUpAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] itemsToAnimate;

    private Vector3[] originalPositions;

    [SerializeField] private PlayableDirector timeline;

    private int i = 0;

    private void Awake()
    {
        this.originalPositions = new Vector3[this.itemsToAnimate.Length];
        
        for(int i = 0; i < this.itemsToAnimate.Length; i++)
        {
            this.originalPositions[i] = this.itemsToAnimate[i].position;
        }
    }

    public void SnapNextItem()
    {
        var j = this.i;
        if (this.i == 4)
        {
            this.timeline.Pause();
        }
        this.itemsToAnimate[j].position.LerpTo(this.originalPositions[j], 1.0f, val => this.itemsToAnimate[j].position = val, null, GlobalLerpProcessor.easeInOutCurve);
        i++;
    }

    public void PlayDirectior() => this.timeline.Play();


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            this.SnapNextItem();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            this.PlayDirectior();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WipeUpAnimation))]
public class WipeUpAnimEditor : Editor
{
    private WipeUpAnimation t;
    private void OnEnable()
    {
        this.t = (WipeUpAnimation)base.target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        
        if (GUILayout.Button("Animate"))
        {
            this.t.SnapNextItem();
        }

        if (GUILayout.Button("Play Directior"))
        {
            this.t.PlayDirectior();
        }
    }
}
#endif