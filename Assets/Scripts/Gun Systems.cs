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

    private Camera playerCamera;

    [SerializeField] private GameObject[] muzzleFlashes;

    private int muzzleFlashIndex = 0;


    [SerializeField] private Animator[] animationControllers;

    private int animationControllerIndex = 0;


    [SerializeField] private Transform[] bulletSpawnLocations;

    private int bulletSpwanLocationIndex = 0;

    private bool tryingToShoot = false;

    private void Start()
    {
        this.gun.PrimeWeaponToShoot();
        this.playerCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (this.tryingToShoot)
        {
            bool bulletShot = this.gun.Shoot(this.bulletSpawnLocations[this.bulletSpwanLocationIndex].position, this.playerCamera.transform.forward);
            if (bulletShot)
            {
                ParticleSystem muzzleFlash = this.muzzleFlashes[this.muzzleFlashIndex].GetComponent<ParticleSystem>();
                muzzleFlash.Play();
                for (int i = 0; i < muzzleFlash.transform.childCount; i++)
                {
                    muzzleFlash.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
                }

                this.muzzleFlashIndex++;
                this.muzzleFlashIndex %= this.muzzleFlashes.Length;
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

    public void OnShootButtonActivate()
    {
        this.tryingToShoot = true;
    }

    public void OnShootButtonDeactiveated()
    {
        this.tryingToShoot = false;
    }
}
