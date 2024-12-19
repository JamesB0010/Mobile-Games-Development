using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RoundStartingInText : MonoBehaviour
{
    private int nextRound = 2;
    private TextMeshProUGUI text;

    private void Awake()
    {
        this.text = GetComponent<TextMeshProUGUI>();
    }

    public void RoundComplete()
    {
        this.text.text = $"Round {this.nextRound} Starting in:";
        this.nextRound++;
    }
}
