using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHeavyWeapon : PlayerShipWeapon
{
    private HeavyGun heavyGun;

    public HeavyGun HeavyGun
    {
        get => this.heavyGun;
        set => this.heavyGun = value;
    }

    protected override void cloneGun()
    {
        this.heavyGun = (HeavyGun)this.heavyGun.Clone();
    }

    protected override Gun getGun()
    {
        return this.heavyGun;
    }

    private void Update()
    {
        this.animator.SetBool(TryingToShoot, gunSystem.tryingToShootHeavy);
        this.animator.SetBool(BulletFired, false);

        if (this.gunSystem.tryingToShootHeavy == false)
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
            bulletShot = this.heavyGun.Shoot(this.bulletSpawnLocation.position, crosshairWorldTargetPosition, true, hit);
        }
        else
        {
            bulletShot = this.heavyGun.Shoot(this.bulletSpawnLocation.position, crosshairWorldTargetPosition, false);
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
