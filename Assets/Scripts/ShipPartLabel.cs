using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ShipPartLabel : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField] private UnityEvent OnZoomingIn = new UnityEvent();

    [SerializeField] private Animation openInventoryAnim;

    [SerializeField] private ShipGunUpgrade[] ShipGunUpgrades;

    [SerializeField] private UpgradeCell[] cells;

    [SerializeField] private int weaponIndex;

    [SerializeField] private ShipSections shipSection;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCam = FindObjectOfType<Camera>();
        Vector3 directionToCamera = transform.position - this.playerCam.transform.position;
        transform.forward = directionToCamera.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToCamera = transform.position - this.playerCam.transform.position;

        transform.forward = directionToCamera.normalized;
    }

    public void Clicked()
    {
        OnZoomingIn?.Invoke();
        openInventoryAnim.Play();

        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i].Upgrade = this.ShipGunUpgrades[i];
            this.cells[i].WeaponIndex = this.weaponIndex;
            this.cells[i].ShipSection = this.shipSection;
        }
    }
}
