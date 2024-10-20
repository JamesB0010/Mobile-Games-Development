using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMaskManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerToChangeTo;

    private Camera sceneCamera;
    void Start()
    {
        this.sceneCamera = FindObjectOfType<Camera>();
    }

    public void ChangeCullMask()
    {
        this.sceneCamera.cullingMask = this.layerToChangeTo;
    }
}
