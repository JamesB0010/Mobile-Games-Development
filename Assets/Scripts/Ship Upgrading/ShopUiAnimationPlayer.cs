using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopUiAnimationPlayer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Animation inventoryAnimationComp;
    
    [SerializeField] private AnimationClip closeInventoryAnim;
    
    [Space(2)]
    [Header("Events")]
    [SerializeField] private UnityEvent CloseInventoryEvent = new UnityEvent();
    
    public void CloseInventory()
    {
        this.inventoryAnimationComp.clip = this.closeInventoryAnim;
        this.inventoryAnimationComp.Play();
        this.CloseInventoryEvent?.Invoke();
    }
}
