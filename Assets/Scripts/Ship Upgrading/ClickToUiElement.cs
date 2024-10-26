using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickToUiElement : MonoBehaviour
{
    
    [SerializeField] private UnityEvent<UpgradeCell> cellSelected = new UnityEvent<UpgradeCell>();
        
    public void OnClick(Vector2 clickPosition)
    {
        List<RaycastResult> raycastResults = PerfromRaycast(clickPosition);

        TriggerClickedObjects(raycastResults);
    }


    private static List<RaycastResult> PerfromRaycast(Vector2 clickPosition)
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = clickPosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        
        return raycastResults;
    }
    
    private void TriggerClickedObjects(List<RaycastResult> raycastResults)
    {
        foreach (var result in raycastResults)
        {
            if (result.gameObject.TryGetComponent(out ShipPartLabel partLabel))
            {
                partLabel.Clicked();
            }
            else if (result.gameObject.TryGetComponent(out UpgradeCell upgradeCell))
            {
                this.cellSelected?.Invoke(upgradeCell);
            }
        }
    }
}
