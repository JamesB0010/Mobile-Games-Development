using Cinemachine;
using UnityEngine;

public class ShopOrbitController : MonoBehaviour
{
    [SerializeField] private Transform rotateAroundPoint;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private float rotationSpeed;

    private Vector2 lastPos = Vector2.zero;

    private bool mouseUp = true;

    public void OnMouseDown(Vector2 pos)
    {
        this.mouseUp = false;
        this.lastPos = pos;
    }

    public void OnMouseUp()
    {
        this.mouseUp = true;
    }

    public void OnScreenPos(Vector2 pos)
    {
        if (mouseUp)
            return;
        
        Vector2 delta = pos - lastPos;
        Debug.Log(delta);
        
        float rotationMagnitudeY = delta.x;
        float rotationMagnitudeZ = delta.y;

        vCam.transform.RotateAround(rotateAroundPoint.position, Vector3.up, rotationMagnitudeY * rotationSpeed * Time.deltaTime);
        vCam.transform.RotateAround(rotateAroundPoint.position, Vector3.right, rotationMagnitudeZ * rotationSpeed * Time.deltaTime);
        
        lastPos = pos; 
    }

    private void Update()
    {
        Debug.Log($"Mouse up is this: {mouseUp} and last pos is this: {lastPos}");
    }
}
