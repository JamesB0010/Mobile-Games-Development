using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GunShopEntrence : MonoBehaviour
{
    public void LoadGunShop()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<PlayableDirector>().Play();
    }
}
