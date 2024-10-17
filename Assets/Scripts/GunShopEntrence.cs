using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GunShopEntrence : MonoBehaviour
{
    private float BorderOutlineWidth;

    private Outline areaOutline;

    private void Start()
    {
        areaOutline = this.GetComponent<Outline>();
        this.BorderOutlineWidth = areaOutline.OutlineWidth;
        areaOutline.OutlineWidth = 0;
    }

    public void OpenGunShop()
    {
        this.areaOutline.OutlineWidth = this.BorderOutlineWidth;
        GetComponent<Collider>().enabled = true;
    }

    public void LoadGunShop()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<PlayableDirector>().Play();
    }
}
