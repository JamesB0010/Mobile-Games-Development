using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorRefToImageInit : MonoBehaviour
{
    [SerializeField] private ColorReference col;

    [SerializeField, Range(0,1)] private float alpha = 1;

    private void Start()
    {
        Color col = this.col.GetValue();
        col.a = this.alpha;
        if (TryGetComponent(out Image img))
        {
            img.color = col;
        }else if (TryGetComponent(out TextMeshProUGUI text))
        {
            text.color = col;
        }
    }
}
