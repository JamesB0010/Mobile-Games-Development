using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Weapon;

public class HeavyGunsAmmoCountView : MonoBehaviour
{
    
    [SerializeField] private UnityEngine.UI.Image[] ammoCounterHeirarchies;
    
    
    private void Start()
    {
        FindObjectOfType<GunSystems>().HeavyGunsInitialied += weapons =>
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].HeavyGun == null)
                {
                    ammoCounterHeirarchies[i].transform.GetChild(0).gameObject.SetActive(false);
                    ammoCounterHeirarchies[i].gameObject.SetActive(false);
                    continue;
                }
                
                
                weapons[i].GunFired += this.OnGunFired;
                this.ammoCounterHeirarchies[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = weapons[i].HeavyGun.MaxAmmoCount.ToString();
            }
        };
    }

    private void OnGunFired(PlayerShipHeavyWeapon obj)
    {
        this.ammoCounterHeirarchies[obj.Index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = obj.HeavyGun.CurrentAmmoCount.ToString();
    }
}
