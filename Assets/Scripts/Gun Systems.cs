using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystems : MonoBehaviour
{
    [SerializeField]
    private Gun gun;

    [SerializeField]
    private AudioSource[] gunshotSoundLocations;

    private int gunshotSoundLocationIndex = 0;

    private Camera camera;


    [SerializeField] private Animator[] animationControllers;

    private int animationControllerIndex = 0;


    [SerializeField] private Transform[] bulletSpawnLocations;

    private int bulletSpwanLocationIndex = 0;

    private bool tryingToShoot = false;
    private void Start()
    {
        this.gun.PrimeWeaponToShoot();
        this.camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (this.tryingToShoot)
        {
            bool bulletShot = this.gun.Shoot(this.bulletSpawnLocations[this.bulletSpwanLocationIndex].position, this.camera.transform.forward);
            if (bulletShot)
            {
                this.animationControllers[this.animationControllerIndex].SetTrigger("BulletFired");
                this.animationControllerIndex++;
                this.animationControllerIndex %= this.animationControllers.Length;
                this.bulletSpwanLocationIndex++;
                this.bulletSpwanLocationIndex %= this.bulletSpawnLocations.Length;
                this.gunshotSoundLocations[gunshotSoundLocationIndex].Play();
                this.gunshotSoundLocationIndex++;
                this.gunshotSoundLocationIndex %= this.gunshotSoundLocations.Length;
            }
        }
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        this.tryingToShoot = ctx.action.IsPressed();
    }
}
