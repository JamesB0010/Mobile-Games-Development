using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   [SerializeField] private PlayableDirector startGameDirector, part1IntroDirector;
   public void EnterGameButtonPressed()
   {
       part1IntroDirector.Play();
   }
   public void IntroFirstPartComplete()
   {
       this.startGameDirector.Play();
   }

}
