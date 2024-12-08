using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerHealthUiElement : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
    }

    public void OnHealthChanged(float newHealth)
    {
        this.text.text = $"Player Health: \n {newHealth}";
    }
}
