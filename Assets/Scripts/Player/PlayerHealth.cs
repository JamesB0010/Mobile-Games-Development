using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100;
    
    [SerializeField] private GameObject DeathParticle;

    public float Health
    {
        get => this.health;

        set
        {
            if (value == this.health)
                return;

            this.health = value;
            this.PlayerHealthChangedEvent?.Invoke(this.health);
        }
    }

    [SerializeField] private UnityEvent<float> PlayerHealthChangedEvent = new UnityEvent<float>();

    [SerializeField] private UnityEvent DeathEvent = new UnityEvent();

    public void OnSelfDestruct()
    {
        this.DeathEvent?.Invoke();
    }


    public void OnShipExplosion()
    {
        Instantiate(this.DeathParticle, transform.position, Quaternion.identity);
    }
}