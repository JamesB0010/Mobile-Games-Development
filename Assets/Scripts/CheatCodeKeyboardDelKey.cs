using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheatCodeKeyboardDelKey : MonoBehaviour
{
    [SerializeField] private CheatCodeInput input;
    public void Delete()
    {
        int stringLength = this.input.Code.Length;
        if (stringLength > 1)
        {
            this.input.Code = this.input.Code.Substring(0, stringLength - 1);
        }
        else
        {
            this.input.Code = "";
        }
    }

}
