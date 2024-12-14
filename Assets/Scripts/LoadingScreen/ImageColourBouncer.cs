using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColourBouncer : MonoBehaviour
{
    [SerializeField] private Color colorFrom;
    [SerializeField] private Color colorTo;

    [SerializeField] private float timeToTake;

    private Image image;

    private void Start()
    {
        this.image = GetComponent<Image>();

        this.image.color = this.colorFrom;
        
        this.image.color.LerpTo(this.colorTo, timeToTake, value =>
        {
            this.image.color = value;
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        });
    }
}
