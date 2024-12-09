using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerElimsTextView : MonoBehaviour
{
    [SerializeField] private IntReference playerKills;

    private TextMeshProUGUI text;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.text.text = this.playerKills.GetValue().ToString();
    }
}
