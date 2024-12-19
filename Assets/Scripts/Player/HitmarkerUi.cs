using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HitmarkerUi : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        this.image = GetComponent<Image>();
        this.image.enabled = false;
    }
    
    public void MarkHit()
    {
        StopCoroutine(nameof(this.DisableHitmarkerAfter));
        this.image.enabled = true;
        StartCoroutine(nameof(this.DisableHitmarkerAfter), 0.1f);
    }

    private IEnumerator DisableHitmarkerAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        this.image.enabled = false;
    }
}
