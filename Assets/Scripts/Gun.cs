using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Gun : ScriptableObject
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private AudioClip gunshotSound;

    private float lastBulletShotTimestamp = -100.0f;

    [SerializeField]
    private float timeBetweenBullets;


    public void PrimeWeaponToShoot()
    {
        this.lastBulletShotTimestamp = -100.0f;
    }
    public bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit)
    {
        if (this.IsPrimedToShoot())
        {
            //shoot
            return true;
        }
        return false;
    }
    public bool Shoot(Vector3 bulletStartPosition, Vector3 forwardDirection)
    {
        if (this.IsPrimedToShoot())
        {
            //instantiate bullet
            Debug.Log("instantiate bullet");
            this.InstantiateBullet(bulletStartPosition, forwardDirection);
            return true;
        }

        return false;
    }

    private bool IsPrimedToShoot()
    {
        bool requiredTimeElapsed = Time.timeSinceLevelLoad - lastBulletShotTimestamp > timeBetweenBullets;
        return requiredTimeElapsed;
    }

    private void InstantiateBullet(Vector3 bulletStartPosition, Vector3 forwardDirection)
    {
        this.UpdateLastBulletShotTimestamp();

        Bullet bullet = Instantiate(this.bulletPrefab, bulletStartPosition, Quaternion.identity);
        
        bullet.transform.localRotation= Quaternion.Euler(forwardDirection);


        SetupBullet(bullet, forwardDirection);

        //PlayBulletShotSound(bulletStartPosition);
    }
    private void UpdateLastBulletShotTimestamp()
    {
        this.lastBulletShotTimestamp = Time.timeSinceLevelLoad;
    }
    private static void SetupBullet(Bullet bullet, Vector3 forwardsDirection)
    {
        bullet.forwards = forwardsDirection;
    }

    private void PlayBulletShotSound(Vector3 bulletStartPosition)
    {
        AudioSource.PlayClipAtPoint(this.gunshotSound, bulletStartPosition);
    }




}
