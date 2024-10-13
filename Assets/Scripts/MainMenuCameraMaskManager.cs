using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MainMenuCameraMaskManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerToChangeTo;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        this.camera = FindObjectOfType<Camera>();
    }

    public void ChangeCullMask()
    {
        this.camera.cullingMask = this.layerToChangeTo;
    }
}
