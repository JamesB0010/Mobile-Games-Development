using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class UsefulPlayerComponents : MonoBehaviour
{
    [SerializeField] private SignalReceiver signalReceiver;

    public SignalReceiver SignalReceiver => this.signalReceiver;


    [SerializeField] private PlayerWallet playerWallet;

    public PlayerWallet PlayerWallet => this.playerWallet; 
    
    [SerializeField] private AnimationPlayExposer rightMonitorAnimPlayExposer;

    public AnimationPlayExposer RightMonitorAnimPlayExposer => this.rightMonitorAnimPlayExposer;

    [SerializeField] private PlayerHapticFeedback playerHapticFeedback;

    public PlayerHapticFeedback PlayerHapticFeedback => this.playerHapticFeedback;

    [SerializeField] private SubtitleSetter subtitleSetter;

    public SubtitleSetter SubtitleSetter => this.subtitleSetter;

    [SerializeField] private OutOfBoundsUiElements outOfBoundsUiElements;

    public OutOfBoundsUiElements OutOfBoundsUiElements => this.outOfBoundsUiElements;
}
