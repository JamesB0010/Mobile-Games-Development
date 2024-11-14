using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Weapon;

public class PlayerShipLightWeapon : PlayerShipWeapon
{
    private LightGun lightGun;

    [SerializeField] private PlayerShipEnergySystem energySystem;

    public LightGun LightGun
    {
        get => this.lightGun;
        set => this.lightGun = value;
    }

    protected override void cloneGun()
    {
        this.lightGun = (LightGun)this.lightGun.Clone();
    }

    protected override Gun getGun()
    {
        return this.lightGun;
    }

    private void Update()
    {
        this.animator.SetBool(TryingToShoot, gunSystem.TryingToShootLight);
        this.animator.SetBool(BulletFired, false);

        if (this.gunSystem.TryingToShootLight == false)
        {
            return;
        }

        if (!this.energySystem.HasBudgetFor(this.lightGun.EnergyExpensePerShot))
            return;

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
            this.energySystem.TryDebitEnergy(this.lightGun.EnergyExpensePerShot);
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
