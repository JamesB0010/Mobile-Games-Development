using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SubtitleSetter : MonoBehaviour
{
    private bool textLocked = false;
    private TextMeshProUGUI text;

    [SerializeField] private ColorReference tertiaryAccentColor;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        if (textLocked)
            return;

        text = text.Replace("<color=\"red\">", $"<color=#{ColorUtility.ToHtmlStringRGB(this.tertiaryAccentColor.GetValue())}>");
        this.text.text = text;
    }

    public void OnUserSubmittedSoundLoader(string status)
    {
        if (status == "Clip Loaded")
        {
            //a user clip has been loaded ignore any attempts to set the text
            this.textLocked = true;
        }
        else
        {
            this.textLocked = false;
        }
    }
}
