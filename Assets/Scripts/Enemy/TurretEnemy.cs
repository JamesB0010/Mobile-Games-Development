using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretEnemy : EnemyBase
{
    public event Action DeathEvent;
    [SerializeField] private GameObject DeathParticle;

    private Transform player;

    [SerializeField] private float maxEngagementDistance;

    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        base.OnDeathReaction = this.OnDeath;
        this.player = FindObjectOfType<GameManager>().Player;
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, this.player.position);
        if (distanceFromPlayer < this.maxEngagementDistance)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.LookRotation(-(this.player.position - transform.position).normalized, transform.up), Time.deltaTime * this.rotationSpeed);
            //shoot at player
            Debug.Log("Shoot at player");
        }
    }


    private void OnDeath()
    {
        DeathEvent?.Invoke();
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
