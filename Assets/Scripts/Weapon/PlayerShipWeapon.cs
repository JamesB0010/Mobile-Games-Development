using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Weapon;

public abstract class PlayerShipWeapon : MonoBehaviour
{
    [SerializeField] protected GunSystems gunSystem;

    [SerializeField] protected AudioSource gunshotSoundLocation;

    [SerializeField] protected ParticleSystem muzzleFlash;

    [SerializeField] protected Animator animator;

    [SerializeField] protected Transform bulletSpawnLocation;

    protected List<ParticleSystem> muzzleFlashParticles = new List<ParticleSystem>();

    public event Action BulletHit;

    protected static readonly int BulletFired = Animator.StringToHash("BulletFired");
    protected static readonly int TryingToShoot = Animator.StringToHash("TryingToShoot");

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

    protected abstract void cloneGun();

    public void ListenToBulletHitEvent()
    {
        if(this.gameObject.activeSelf)
            this.getGun().BulletReachedDesitnation += this.BulletHit;
    }

    protected abstract Gun getGun();

    public void SetupWeapon()
    {
        this.cloneGun();
        this.getGun().PrimeWeaponToShoot();
        this.cacheMuzzleFlashParticles();
    }
}
