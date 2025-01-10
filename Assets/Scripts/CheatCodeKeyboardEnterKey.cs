using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeKeyboardEnterKey : MonoBehaviour
{
    [SerializeField] private CheatCodesManager manager;

    [SerializeField] private CheatCodeInput input;

    [SerializeField] private GameObject cheatCodeAppliedScreen;
    public void Enter()
    {
        bool sucess = this.manager.TryApplyCheatCode(input.Code);
        this.input.Clear();
        if(sucess)
            this.cheatCodeAppliedScreen.SetActive(true);
    }
}
