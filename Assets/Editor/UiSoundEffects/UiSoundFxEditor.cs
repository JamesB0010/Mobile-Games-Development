using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(UiAudioSounds))]
public class UiSoundFxEditor : Editor
{
    private UiAudioSounds audioSounds;

    private AudioClip testAudioClip;

    private static bool testAreaExpanded;
    
    [SerializeField] private VisualTreeAsset uxml;
    private ObjectField testClipField;

    public override VisualElement CreateInspectorGUI()
    {
        this.audioSounds = (UiAudioSounds)base.target;
        VisualElement root = uxml.CloneTree();

        root.Q<Button>("DefaultClickButton").clicked += () =>
        {
            this.audioSounds.PlayDefaultClickSound();
        };

        root.Q<Button>("fuzzyClickButton").clicked += () =>
        {
            this.audioSounds.PlayFuzzyClickSound();
        };

        root.Q<Button>("playToggleOn").clicked += () =>
        {
            this.audioSounds.PlayToggleOnSound();
        };
        
        root.Q<Button>("playToggleOff").clicked += () =>
        {
            this.audioSounds.PlayToggleOffSound();
        };
        

        testClipField = root.Q<ObjectField>("TestAudioClip");
        testClipField.value = this.testAudioClip;

        testClipField.RegisterValueChangedCallback(e =>
        {
            this.testAudioClip = e.newValue as AudioClip;
        });

        root.Q<Button>("TestClipButton").clicked += () =>
        {
            this.audioSounds.PlayClip(this.testAudioClip);
        };

        var foldout = root.Q<Foldout>("TestAudioFoldout");
        foldout.value = testAreaExpanded;
        foldout.RegisterValueChangedCallback(e =>
        {
            testAreaExpanded = e.newValue;
        });

        Selection.selectionChanged += this.OnSelectionChanged;
        
        return root;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= this.OnSelectionChanged;
    }

    private void OnSelectionChanged()
    {
        if (Selection.activeObject is AudioClip clip)
        {
            this.testAudioClip = clip;
            this.testClipField.value = clip;
        }
    }
}
