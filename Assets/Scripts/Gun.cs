using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Gun : ScriptableObject
{
    [SerializeField]
    private Bullet bulletPrefab;

    private float lastBulletShotTimestamp = -100.0f;

    [SerializeField]
    private float timeBetweenBullets;

    [SerializeField]
    private bool ownedByPlayer;

    public bool OwnedByPlayer
    {
        get => this.ownedByPlayer;

        set => this.ownedByPlayer = value;
    }


    [SerializeField] private float cost;

    public float Cost => this.cost;

    [SerializeField] private float bulletDamage;

    public void PrimeWeaponToShoot()
    {
        this.lastBulletShotTimestamp = -100.0f;
    }
    public bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit)
    {
        if (this.IsPrimedToShoot())
        {
            Bullet bullet = InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            bullet.hit = hit;
            return true;
        }
        return false;
    }
    public bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
    {
        if (this.IsPrimedToShoot())
        {
            this.InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            return true;
        }

        return false;
    }

    private bool IsPrimedToShoot()
    {
        bool requiredTimeElapsed = Time.timeSinceLevelLoad - lastBulletShotTimestamp > timeBetweenBullets;
        return requiredTimeElapsed;
    }

    private Bullet InstantiateBullet(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
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

}
