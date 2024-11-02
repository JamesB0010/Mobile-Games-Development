using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : ShipItem
{
    [SerializeField]
    protected Bullet bulletPrefab;
    protected float lastBulletShotTimestamp = -100.0f;

    [SerializeField]
    protected float timeBetweenBullets;

    public float TimeBetweenBullets => this.timeBetweenBullets;

    [SerializeField] protected float bulletDamage;
    public float BulletDamage => this.bulletDamage;

    protected void CloneGunSharedAttributes(Gun gun)
    {
        gun.bulletDamage = bulletDamage;
                        gun.bulletPrefab = bulletPrefab;
                        gun.lastBulletShotTimestamp = lastBulletShotTimestamp;
                        gun.timeBetweenBullets = timeBetweenBullets;
    }

    public abstract bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit);

    public abstract bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget);

    protected bool IsPrimedToShoot()
    {
        bool requiredTimeElapsed = Time.timeSinceLevelLoad - lastBulletShotTimestamp > timeBetweenBullets;
        return requiredTimeElapsed;
    }

    public void PrimeWeaponToShoot()
    {
        this.lastBulletShotTimestamp = -100.0f;
    }

    protected void UpdateLastBulletShotTimestamp()
    {
        this.lastBulletShotTimestamp = Time.timeSinceLevelLoad;
    }

    private void SetupBullet(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, Bullet bullet)
    {
        bullet.SetupBulletData(hasValidTarget, this.bulletDamage, bulletStartPosition, targetPosition);
    }

    protected Bullet InstantiateBullet(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
    {
        this.UpdateLastBulletShotTimestamp();

        Bullet bullet = Instantiate(this.bulletPrefab, bulletStartPosition, Quaternion.identity);

        SetupBullet(bulletStartPosition, targetPosition, hasValidTarget, bullet);

        return bullet;
    }
}
