using UnityEngine;
using UnityEngine.UI;

public class CrosshairPositioner : MonoBehaviour
{
    private bool firstPerson = true;

    [SerializeField] private Transform lookAtPoint; // World-space object
    [SerializeField] private Camera thirdPersonCamera; // The camera rendering the object
    [SerializeField] private RectTransform canvasRectTransform; // Canvas RectTransform

    private RectTransform imageTransform; // UI element RectTransform

    private void Start()
    {
        this.imageTransform = GetComponent<Image>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (this.firstPerson)
        {
            this.imageTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            Vector3 screenPoint = this.thirdPersonCamera.WorldToScreenPoint(this.lookAtPoint.position);

            //Get the canvas size in pixels
            Vector2 canvasSize = canvasRectTransform.sizeDelta;

            // Map screen position to canvas local space
            Vector2 canvasPosition = new Vector2(
                screenPoint.x - (Screen.width / 2f),
                screenPoint.y - (Screen.height / 2f)
            );

            // Scale to match the canvas size
            canvasPosition.x *= canvasSize.x / Screen.width;
            canvasPosition.y *= canvasSize.y / Screen.height;

            // Assign the new position
            this.imageTransform.anchoredPosition = canvasPosition;
        }
    }

    public void CameraViewChanged(bool firstPerson)
    {
        this.firstPerson = firstPerson;
    }
}