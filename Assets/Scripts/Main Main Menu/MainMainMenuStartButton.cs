using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMainMenuStartButton : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField] private Image blackoutImage;

    [SerializeField] private SceneChanger sceneChanger;

    private void Awake()
    {
        this.text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        this.text.fontSize.LerpTo(this.text.fontSize + 2f, 3f, val => this.text.fontSize = val, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        }, GlobalLerpProcessor.easeInOutCurve);
    }

    public void Pressed()
    {
        this.blackoutImage.gameObject.SetActive(true);
        this.blackoutImage.color.a.LerpTo(1, 0.3f, val => this.blackoutImage.color = new Color(0, 0, 0, val), pkg =>
        {
            this.sceneChanger.LoadSceneSingle(2);
        });
    }
}
