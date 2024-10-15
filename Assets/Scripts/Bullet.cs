using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private class BulletMovementData
    {
        public Vector3 startPosition;
        public Vector3 destination;
        public Vector3 direction;
        public float speed = 500;
    }

    //Attributes
    [SerializeField]
    private GameObject bulletImpactPrefab;

    private bool hasValidTarget;

    private BulletMovementData bulletMovementData = new BulletMovementData();

    public RaycastHit hit;

    //methods
    public void SetupBulletData(bool bulletHasValidTarget, Vector3 startPosition, Vector3 bulletDestination)
    {
        this.hasValidTarget = bulletHasValidTarget;
        this.bulletMovementData.startPosition = startPosition;
        this.bulletMovementData.destination = bulletDestination;
        this.bulletMovementData.direction = (this.bulletMovementData.destination - transform.position).normalized;

        this.OrientBullet();
        this.setDestinationIfInvalidTarget();
    }
    private void OrientBullet()
    {
        transform.forward = this.bulletMovementData.direction;
    }

    public void setDestinationIfInvalidTarget()
    {
        if (hasValidTarget == false)
            bulletMovementData.destination = bulletMovementData.direction * 150.0f;
    }

    void Update()
    {
        StepBulletForwards();

        if (BulletHasReachedDestination())
        {
            TerminateBullet();
        }
    }
    private void StepBulletForwards()
    {
        Vector3 distanceToMove = this.bulletMovementData.direction * (this.bulletMovementData.speed * Time.deltaTime);

        transform.position += distanceToMove;
    }

    private bool BulletHasReachedDestination()
    {
        return Vector3.Dot(this.bulletMovementData.direction, (this.bulletMovementData.destination - transform.position).normalized) <= 0;
    }
    private void TerminateBullet()
    {
        if (this.hasValidTarget)
        {
            Destroy(gameObject);
            bool targetStillExists = false;

            EnemyBase enemyBase = null;

            try
            {
                targetStillExists = this.hit.collider.TryGetComponent(out enemyBase);
            }
            catch (NullReferenceException) { }


            if (targetStillExists)
            {
                enemyBase.TakeDamage(3f);
            }
            SpawnImpactParticle();
        }
        else
        {
            //bullet shot at the sky
            Destroy(gameObject);
        }
    }

    private void SpawnImpactParticle()
    {
        GameObject bulletParticleSystem =
            Instantiate(this.bulletImpactPrefab, this.bulletMovementData.destination, Quaternion.identity);

        //offset the impact particle so that it isnt intersecting with the object we hit
        float particleSystemOffsetDistance = 0.1f;
        bulletParticleSystem.transform.position = bulletParticleSystem.transform.position +
                                                  (this.hit.normal *
                                                   particleSystemOffsetDistance);
    }
}
