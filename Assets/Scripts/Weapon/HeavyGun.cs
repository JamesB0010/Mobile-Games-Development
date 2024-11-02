using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Guns/Heavy Gun")]
public class HeavyGun : Gun
{
    [SerializeField] private bool ableToShoot;
    public override bool Shoot(Vector3 bulletStartPosition, Vector3 targetPosition, bool hasValidTarget, RaycastHit hit)
    {
        if (!ableToShoot)
            return false;

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
        if (!ableToShoot)
            return false;

        if (this.IsPrimedToShoot())
        {
            this.InstantiateBullet(bulletStartPosition, targetPosition, hasValidTarget);
            return true;
        }

        return false;
    }

    public override object Clone()
    {
        HeavyGun obj = ScriptableObject.CreateInstance<HeavyGun>();
        base.CloneGunSharedAttributes(obj);
        obj.ableToShoot = this.ableToShoot;
        return obj;
    }
}

