using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInMainMenu : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        this.image = GetComponent<Image>();
        this.image.color = Color.black;
    }

    private void Start()
    {
        this.image.color.a.LerpTo(0, 0.3f, val => this.image.color = new Color(0, 0, 0, val));
    }
}
