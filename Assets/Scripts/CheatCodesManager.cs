using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodesManager : MonoBehaviour
{
    [SerializeField] private KeyValuePairWrapper<string, CheatCodeEffect>[] cheatCodes;

    private void Start()
    {
        for (int i = 0; i < this.cheatCodes.Length; i++)
        {
            this.cheatCodes[i].key = this.cheatCodes[i].key.ToLower();
        }

    }

    public bool TryApplyCheatCode(string inputCode)
    {
        inputCode = inputCode.ToLower();
        foreach (var codeAndEffect in this.cheatCodes)
        {
            if (codeAndEffect.key == inputCode)
            {
                codeAndEffect.value.Execute();
                return true;
            }
        }
        return false;
    }
}
