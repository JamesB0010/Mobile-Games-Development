using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMaskManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerToChangeTo;

    [SerializeField] private Camera sceneCamera;

    public void ChangeCullMask()
    {
        this.sceneCamera.cullingMask = this.layerToChangeTo;
    }
}
