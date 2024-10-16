using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunStorePlayerMoneyDisplayer : MonoBehaviour
{
    [SerializeField]
    private FloatReference playerMoney;

    private TextMeshProUGUI textField;

    private void Start()
    {
        this.textField = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.textField.text = "Player Money: " + (float)playerMoney.GetValue();
    }
}
