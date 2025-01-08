using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour
{
    protected char character;

    [SerializeField] protected CheatCodeInput cheatCodeInput;

    [SerializeField] protected bool Normalkey = true;
    
    private void Awake()
    {
        this.character = this.gameObject.name[0];
        if(this.Normalkey)
            GetComponent<Button>().onClick.AddListener(this.OnPressed);
    }

    public void OnPressed()
    {
        if (this.Normalkey)
        {
            this.cheatCodeInput.Code += this.character;
        }
    }
}
