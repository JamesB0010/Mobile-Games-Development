using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutOfBoundsUiElements : MonoBehaviour
{
   [SerializeField] private Image blackoutImage;

   [SerializeField] private TextMeshProUGUI text, timer;

   [SerializeField] private Image skull;


   private Color blackoutImageColor, textColor, timerColor, skullColor;
   private void Start()
   {
      this.blackoutImageColor = this.blackoutImage.color;
      this.textColor = this.text.color;
      this.timerColor = this.timer.color;
      this.skullColor = this.skull.color;
   }

   public void PlayerExitedBounds(FloatLerpPackage timeout)
   {
      timeout.onLerpStep += FadeInWarning;
      this.timer.text = "20";
   }

   private void FadeInWarning(float value)
   {
      Debug.Log(value);
      this.blackoutImage.color = new Color(blackoutImageColor.r, blackoutImageColor.g, blackoutImageColor.b, value);
      this.text.color = new Color(textColor.r, textColor.g, textColor.b, value);
      this.timer.color = new Color(timerColor.r, timerColor.g, timerColor.b, value);
      this.timer.text = (20 - value * 20).ToString();
      this.skull.color = new Color(skullColor.r, skullColor.g, skullColor.b, value);
   }

   public void PlayerBackInBounds()
   {
      this.blackoutImage.color = blackoutImageColor;
      this.text.color = this.textColor;
      this.timer.color = this.timerColor;
      this.skull.color = this.skullColor;
   }
}
