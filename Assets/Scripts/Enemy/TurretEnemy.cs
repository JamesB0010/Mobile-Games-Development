using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyBase
{
    [SerializeField] private GameObject DeathParticle;

    private bool dead = false;

    public event Action DeathEvent;

    protected override void OnDeath()
    {
        if (this.dead)
            return;

        this.dead = true;
        DeathEvent?.Invoke();
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
