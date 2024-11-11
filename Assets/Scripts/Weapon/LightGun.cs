using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Guns/Light Gun")]
public class LightGun : Gun
{
    [SerializeField] private float energyExpensePerShot;

    public float EnergyExpensePerShot => this.energyExpensePerShot;
    
    public override bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit)
    {
        if (this.IsPrimedToShoot())
        {
            Bullet bullet = InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            bullet.hit = hit;
            return true;
        }
        return false;
    }
    public override bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget)
    {
        if (this.IsPrimedToShoot())
        {
            this.InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            return true;
        }

        return false;
    }

    public override object Clone()
    {
        LightGun obj = ScriptableObject.CreateInstance<LightGun>();
        base.CloneGunSharedAttributes(obj);
        return obj;
    }
}


