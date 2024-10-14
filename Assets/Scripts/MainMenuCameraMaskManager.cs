using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MainMenuCameraMaskManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerToChangeTo;

    private Camera sceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.sceneCamera = FindObjectOfType<Camera>();
    }

    public void ChangeCullMask()
    {
        this.sceneCamera.cullingMask = this.layerToChangeTo;
    }
}
