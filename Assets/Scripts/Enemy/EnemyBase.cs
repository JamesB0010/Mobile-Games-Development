using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;

    protected Action OnDeathReaction = null;


    private bool dead = false;

    protected float Health
    {
        get => this.health;

        set
        {
            this.health = value;

            if (this.health <= 0)
            {
                this.OnDeath();
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void TakeDamage(float damageToTake)
    {
        this.Health = this.health - damageToTake;
    }

    private void OnDeath()
    {
        if (this.dead)
            return;
        this.dead = true;

        this.OnDeathReaction?.Invoke();
    }
}