using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerCreditsTextView : MonoBehaviour
{
    [SerializeField] private FloatReference playerMoney;

    private TextMeshProUGUI text;
    
    void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.text.text = playerMoney.GetValue().ToString();
        this.playerMoney.valueChanged += this.OnPlayerMoneyChanged;
    }

    private void OnPlayerMoneyChanged(float newValue)
    {
        this.text.text = newValue.ToString();
    }
}
