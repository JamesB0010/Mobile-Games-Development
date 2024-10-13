using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elementsToHide;
    public void HideUI()
    {
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            this.elementsToHide[i].gameObject.SetActive(false);
        }
    }
}
