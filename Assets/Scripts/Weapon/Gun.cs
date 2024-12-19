using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : ShipItem
{
    
    [SerializeField] private bool ableToShoot;
    
    public bool AbleToShoot => this.ableToShoot;
    [SerializeField]
    protected Bullet bulletPrefab;
    protected float lastBulletShotTimestamp = -100.0f;
    
    [SerializeField] private int firepower;

    public int Firepower => this.firepower;

    [SerializeField]
    protected float timeBetweenBullets;

    public float TimeBetweenBullets => this.timeBetweenBullets;

    [SerializeField] protected float bulletDamage;
    public float BulletDamage => this.bulletDamage;

    public event Action BulletReachedDesitnation;
    

     public bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit)
    {
        if (!ableToShoot)
            return false;

        if (this.IsPrimedToShoot())
        {
            Bullet bullet = InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            bullet.BulletReachedDestination += this.BulletReachedDesitnation;
            bullet.hit = hit;
            return true;
        }
        return false;
    }
    public bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
    {
        if (!ableToShoot)
            return false;

        if (this.IsPrimedToShoot())
        {
            this.InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            return true;
        }

        return false;
    }
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
