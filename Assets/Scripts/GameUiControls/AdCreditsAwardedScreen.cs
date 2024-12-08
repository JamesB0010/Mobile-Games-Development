using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdCreditsAwardedScreen : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI creditsAwarded, newBalance;

    [SerializeField] private FloatReference playerMoney;
    
    public void DisplayAward(int creditsAwarded)
    {
        this.gameObject.SetActive(true);
        this.creditsAwarded.text = $"Credits awarded: {creditsAwarded}";

        this.newBalance.text = $"New Balance: {this.playerMoney.GetValue()}";
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }
}
