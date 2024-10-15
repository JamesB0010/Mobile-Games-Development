using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] private GameObject DeathParticle;


    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float speed;

    [SerializeField] private float maxVelocity;
    private void Update()
    {
        Vector3 acceleration = transform.forward * (Time.deltaTime * speed);
        this.velocity += acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.maxVelocity);
        transform.Translate(this.velocity);
    }

    protected override void OnDeath()
    {
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
