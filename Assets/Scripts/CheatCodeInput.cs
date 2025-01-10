using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class CheatCodeInput : MonoBehaviour
{
    private string code = "";

    [SerializeField] private int maxLength;

        
    private TMP_InputField inputField;

    public string Code
    {
        get => code;

        set
        {
            if (value.Length > maxLength)
                return;
            
            
            this.code = value;
            this.inputField.text = value;
        }
    }

    private void Start()
    {
        this.inputField = FindObjectOfType<TMP_InputField>();
    }

    public void Clear()
    {
        this.Code = "";
    }
}
