using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Weapon;

public class PlayerShipWeapon : MonoBehaviour
{
    [FormerlySerializedAs("gun")]
    [SerializeField]
    private LightGun lightGun;

    public LightGun LightGun
    {
        set => this.lightGun = value;
    }

    [SerializeField] private GunSystems gunSystem;

    [SerializeField] private AudioSource gunshotSoundLocation;

    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform bulletSpawnLocation;

    private List<ParticleSystem> muzzleFlashParticles = new List<ParticleSystem>();

    private static readonly int BulletFired = Animator.StringToHash("BulletFired");
    private static readonly int TryingToShoot = Animator.StringToHash("TryingToShoot");

    private void Start()
    {
        this.lightGun = (LightGun)this.lightGun.Clone();
        this.lightGun.PrimeWeaponToShoot();
        this.cacheMuzzleFlashParticles();
    }

    private void cacheMuzzleFlashParticles()
    {
        for (int i = 0; i < this.muzzleFlash.transform.childCount; i++)
        {
            if (this.muzzleFlash.transform.GetChild(i).TryGetComponent(out ParticleSystem system))
            {
                this.muzzleFlashParticles.Add(system);
            }
        }
    }

    private void Update()
    {
        this.animator.SetBool(TryingToShoot, gunSystem.TryingToShoot);
        this.animator.SetBool(BulletFired, false);

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
            bulletShot = this.lightGun.Shoot(this.bulletSpawnLocation.position, crosshairWorldTargetPosition, true, hit);
        }
        else
        {
            bulletShot = this.lightGun.Shoot(this.bulletSpawnLocation.position, crosshairWorldTargetPosition, false);
        }

        if (bulletShot)
        {
            this.muzzleFlash.Play();
            for (int i = 0; i < this.muzzleFlashParticles.Count; i++)
            {
                this.muzzleFlashParticles[i].Play();
            }

            this.animator.SetBool(BulletFired, true);

            this.gunshotSoundLocation.Play();
        }
    }
}
