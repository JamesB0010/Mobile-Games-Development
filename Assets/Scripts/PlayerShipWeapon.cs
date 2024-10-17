using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipWeapon : MonoBehaviour
{
    [SerializeField]
    private Gun gun;

    [SerializeField] private GunSystems gunSystem;

    private void Start()
    {
        this.gun = (Gun)this.gun.Clone();
        this.gun.PrimeWeaponToShoot();
    }

    [SerializeField] private AudioSource gunshotSoundLocation;

    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform bulletSpawnLocation;
    
    private void Update()
    {
        this.animator.SetBool("TryingToShoot", gunSystem.TryingToShoot);
        this.animator.SetBool("BulletFired", false);

        if (this.gunSystem.TryingToShoot == false)
        {
            return;
        }
        

        bool bulletShot = false;
        CrosshairTargetFinder crosshairTargetFinder = this.gunSystem.CrosshairTargetFinder;
            Vector3 crosshairWorldTargetPosition = crosshairTargetFinder.GetLatestHitPosition();
            bool lastHitValid = crosshairTargetFinder.WasLastHitValid();
            if (lastHitValid)
            {
                RaycastHit hit = crosshairTargetFinder.GetLastHit();
                bulletShot = this.gun.Shoot(this.bulletSpawnLocation.position, crosshairWorldTargetPosition, true, hit);
            }
            else
            {
                bulletShot = this.gun.Shoot(this.bulletSpawnLocation.position, crosshairWorldTargetPosition, false);
            }

            if (bulletShot)
            {
                this.muzzleFlash.Play();
                for (int i = 0; i < muzzleFlash.transform.childCount; i++)
                {
                    //Make this more efficient
                    muzzleFlash.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
                }
                
                this.animator.SetBool("BulletFired", true);
                
                this.gunshotSoundLocation.Play();
            }
    }
}
