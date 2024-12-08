using Cinemachine;
using UnityEngine;

public class ShopOrbitController : MonoBehaviour
{
    [SerializeField] private Transform rotateAroundPoint;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxLookDown, maxLookUp;

    private Vector2 lastPos = Vector2.zero;

    private bool mouseUp = true;

    private float mouseDownTime;

    public void OnMouseDown(Vector2 pos)
    {
        this.mouseUp = false;
        this.mouseDownTime = Time.timeSinceLevelLoad;
    }

    public void OnMouseUp()
    {
        this.mouseUp = true;
    }

    public void OnScreenPos(Vector2 pos)
    {
        if (mouseUp || Time.timeSinceLevelLoad - this.mouseDownTime == 0)
        {
            lastPos = pos;
            return;
        }
    
        Vector2 delta = pos - lastPos;
        delta.x = delta.x;
        Debug.Log(delta);
    
        float rotationMagnitudeY = delta.x;

        // Normalize the x rotation to [-180, 180] range
        float normalizedXRotation = NormalizeAngle(vCam.transform.rotation.eulerAngles.x);

        bool noMoreLookingDown = normalizedXRotation >= maxLookDown && delta.y < 0;
        bool noMoreLookingUp = normalizedXRotation <= maxLookUp && delta.y > 0;

        float rotationMagnitudeX = noMoreLookingDown || noMoreLookingUp ? 0 : delta.y;
    
        vCam.transform.RotateAround(rotateAroundPoint.position, -vCam.transform.up, -rotationMagnitudeY * rotationSpeed * Time.deltaTime);
        vCam.transform.RotateAround(rotateAroundPoint.position, vCam.transform.right, -rotationMagnitudeX * rotationSpeed * Time.deltaTime);

        // Ignore z rotation
        Vector3 camRotation = vCam.transform.eulerAngles;
        vCam.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);

        lastPos = pos; 
    }

// Helper method to normalize angles to [-180, 180]
    private float NormalizeAngle(float angle)
    {
        if (angle > 180)
            angle -= 360;
        return angle;
    }

}
