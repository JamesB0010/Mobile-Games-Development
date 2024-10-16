using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGameFromGunStore : MonoBehaviour
{
    public void ReturnToGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
