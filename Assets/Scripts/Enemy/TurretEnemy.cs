using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CrosshairTargetFinder))]
public class TurretEnemy : EnemyBase
{
    public event Action DeathEvent;
    [SerializeField] private GameObject DeathParticle;

    private Transform player;

    [SerializeField] private float maxEngagementDistance;

    [SerializeField] private float rotationSpeed;

    private CrosshairTargetFinder crosshairTargetFinder;

    [SerializeField] private EnemyGun gun;

    private void Start()
    {
        base.OnDeathReaction = this.OnDeath;
        this.player = FindObjectOfType<GameManager>().Player;
        this.gun = this.gun.Clone() as EnemyGun;
        this.crosshairTargetFinder = GetComponent<CrosshairTargetFinder>();
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, this.player.position);
        if (distanceFromPlayer < this.maxEngagementDistance)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.LookRotation((this.player.position - transform.position).normalized, transform.up), Time.deltaTime * this.rotationSpeed);

            if (crosshairTargetFinder.WasLastHitValid())
            {
                //shoot at player
                this.gun.Shoot(transform.position, this.crosshairTargetFinder.GetLatestHitPosition(),true);
            }
        }
    }


    private void OnDeath()
    {
        DeathEvent?.Invoke();
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
