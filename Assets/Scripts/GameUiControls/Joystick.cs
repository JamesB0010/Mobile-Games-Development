using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool focused;
    private int activePointerId = -1;

    private Vector2 currentPos;
    private Vector2 center;
    [SerializeField] private float radius; // Max deflection for both axes

    /// <summary>
    /// This essentially allows the joystick to start from the bottom y instead of the center
    /// useful for throttle controls
    /// </summary>
    [SerializeField] private bool bottomIsZero;

    public UnityEvent<Vector2> MovementDetected;

    private void Start()
    {
        this.center = this.transform.position;

        
        transform.position = GetDefaultPosition();

        this.currentPos = this.transform.position;
    }

    private Vector2 GetDefaultPosition()
    {
        return this.bottomIsZero ? new Vector2(this.center.x, (this.center + (Vector2.down * this.radius)).y) : new Vector2(this.center.x, this.center.y);
    }

    private void UpdatePos(Vector2 pointerPos)
    {
        // Clamp X and Y independently
        pointerPos = ClampPositionWithinSquare(pointerPos);

        this.currentPos = pointerPos;
        this.MovementDetected?.Invoke(PositionToNormalisedDirection());

        this.transform.position = pointerPos;
    }

    private Vector2 ClampPositionWithinSquare(Vector2 pointerPos)
    {
        Vector2 offset = pointerPos - this.center;

        // Clamp X and Y independently to the range [-radius, radius]
        offset.x = Mathf.Clamp(offset.x, -this.radius, this.radius);
        offset.y = Mathf.Clamp(offset.y, -this.radius, this.radius);

        return this.center + offset;
    }

    public Vector2 PositionToNormalisedDirection()
    {
        Vector2 centerToCurrent = this.currentPos - this.center;

        // Normalize X separately to [-1, 1]
        float normalizedX = Mathf.Clamp(centerToCurrent.x / this.radius, -1f, 1f);

        float normalizedY;
        if (this.bottomIsZero)
        {
            // Normalize Y to the range [0, 1]
            // Bottom (-radius) maps to 0, center maps to 0.5, top (+radius) maps to 1
            normalizedY = Mathf.Clamp((centerToCurrent.y + this.radius) / (2 * this.radius), 0f, 1f);
        }
        else
        {
            normalizedY = Mathf.Clamp(centerToCurrent.y / this.radius, -1f, 1f);
        }

        return new Vector2(normalizedX, normalizedY);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!focused)
        {
            focused = true;
            activePointerId = eventData.pointerId;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (focused && eventData.pointerId == activePointerId)
        {
            UpdatePos(eventData.position);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == activePointerId)
        {
            focused = false;
            activePointerId = -1;

            transform.position = this.GetDefaultPosition();
            this.currentPos = this.transform.position;

            this.MovementDetected?.Invoke(this.PositionToNormalisedDirection());
        }
    }
}
