using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GunShopEntrence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<PlayableDirector>().Play();
    }
}
