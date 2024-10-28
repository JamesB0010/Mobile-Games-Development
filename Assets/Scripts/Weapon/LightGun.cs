using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Guns/Light Gun")]
public class LightGun : ScriptableObject, ICloneable
{
    [SerializeField]
    private Bullet bulletPrefab;

    private float lastBulletShotTimestamp = -100.0f;

    [SerializeField]
    private float timeBetweenBullets;

    public float TimeBetweenBullets
    {
        get => this.timeBetweenBullets;
    }

    [SerializeField] private float bulletDamage;

    public float BulletDamage => this.bulletDamage;

    public void PrimeWeaponToShoot()
    {
        this.lastBulletShotTimestamp = -100.0f;
    }
    public virtual bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit)
    {
        if (this.IsPrimedToShoot())
        {
            Bullet bullet = InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            bullet.hit = hit;
            return true;
        }
        return false;
    }
    public virtual bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
    {
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

    protected Bullet InstantiateBullet(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
    {
        this.UpdateLastBulletShotTimestamp();

        Bullet bullet = Instantiate(this.bulletPrefab, bulletStartPosition, Quaternion.identity);

        SetupBullet(bulletStartPosition, targetPosition, hasValidTarget, bullet);

        return bullet;
    }
    private void UpdateLastBulletShotTimestamp()
    {
        this.lastBulletShotTimestamp = Time.timeSinceLevelLoad;
    }
    private void SetupBullet(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, Bullet bullet)
    {
        bullet.SetupBulletData(hasValidTarget, this.bulletDamage, bulletStartPosition, targetPosition);
    }

    public object Clone()
    {
        LightGun obj = ScriptableObject.CreateInstance<LightGun>();
        obj.bulletDamage = bulletDamage;
        obj.bulletPrefab = bulletPrefab;
        obj.lastBulletShotTimestamp = lastBulletShotTimestamp;
        obj.timeBetweenBullets = timeBetweenBullets;
        return obj;
    }
}


