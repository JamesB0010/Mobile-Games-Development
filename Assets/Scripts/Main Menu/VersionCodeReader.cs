using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionCodeReader : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Version {Application.version}";
    }
}