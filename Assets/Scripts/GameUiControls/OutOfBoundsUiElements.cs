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

   public void PlayerExitedBounds(FloatLerpPackage timeout)
   {
      timeout.onLerpStep += FadeInWarning;
      this.timer.text = "20";
   }

   private void FadeInWarning(float value)
   {
      Debug.Log(value);
      Color blackoutImageColor = this.blackoutImage.color;
      Color textColor = this.text.color;
      Color timerColor = this.timer.color;
      Color skullColor = this.skull.color;
      this.blackoutImage.color = new Color(blackoutImageColor.r, blackoutImageColor.g, blackoutImageColor.b, value);
      this.text.color = new Color(textColor.r, textColor.g, textColor.b, value);
      this.timer.color = new Color(timerColor.r, timerColor.g, timerColor.b, value);
      this.timer.text = (20 - value * 20).ToString();
      this.skull.color = new Color(skullColor.r, skullColor.g, skullColor.b, value);
   }

   public void PlayerBackInBounds()
   {
      Color blackoutImageCol = this.blackoutImage.color;
      blackoutImageCol.a = 0;

      Color textColor = this.text.color;
      textColor.a = 0;

      Color timerColor = this.timer.color;
      timerColor.a = 0;

      Color skullColor = this.skull.color;
      skullColor.a = 0;
      
      
      this.blackoutImage.color = blackoutImageCol;
      this.text.color = textColor;
      this.timer.color = timerColor;
      this.skull.color = skullColor;
   }
}
