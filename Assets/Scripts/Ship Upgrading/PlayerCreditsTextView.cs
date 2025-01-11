using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerCreditsTextView : MonoBehaviour
{
    private TextMeshProUGUI text;
    
    void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        FloatReference playerMoney = BuzzardGameData.PlayerMoney;
        this.text.text = playerMoney.GetValue().ToString();
        playerMoney.valueChanged += this.OnPlayerMoneyChanged;
    }

    private void OnPlayerMoneyChanged(float newValue)
    {
        this.text.text = newValue.ToString();
    }
}
