using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerElimsTextView : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.text.text = BuzzardGameData.PlayerKills.GetValue().ToString();
    }
}
