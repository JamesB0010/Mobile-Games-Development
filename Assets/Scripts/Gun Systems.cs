using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CrosshairTargetFinder))]
public class GunSystems : MonoBehaviour
{
    [SerializeField]
    private Gun gun;

    [SerializeField]
    private AudioSource[] gunshotSoundLocations;

    private int gunshotSoundLocationIndex = 0;

    private CrosshairTargetFinder crosshairTargetFinder;

    private Camera playerCamera;

    [SerializeField] private GameObject[] muzzleFlashes;

    private int muzzleFlashIndex = 0;


    [SerializeField] private Animator[] animationControllers;

    private int animationControllerIndex = 0;


    [SerializeField] private Transform[] bulletSpawnLocations;

    private int bulletSpwanLocationIndex = 0;

    private bool tryingToShoot = false;
    private static readonly int TryingToShoot = Animator.StringToHash("TryingToShoot");

    private void Start()
    {
        this.gun.PrimeWeaponToShoot();
        this.playerCamera = FindObjectOfType<Camera>();
        this.crosshairTargetFinder = GetComponent<CrosshairTargetFinder>();
    }

    private void Update()
    {
        Debug.Log(this.tryingToShoot);
        foreach (var animatorController in this.animationControllers)
        {
            animatorController.SetBool(TryingToShoot, this.tryingToShoot);
            animatorController.SetBool("BulletFired", false);
        }
        bool bulletShot = false;
        
        if (this.tryingToShoot)
        {
            Vector3 crosshairWorldTargetPosition = this.crosshairTargetFinder.GetLatestHitPosition();
            bool lastHitValid = this.crosshairTargetFinder.WasLastHitValid();
            if (lastHitValid)
            {
                RaycastHit hit = this.crosshairTargetFinder.GetLastHit();
                bulletShot = this.gun.Shoot(this.bulletSpawnLocations[this.bulletSpwanLocationIndex].position, crosshairWorldTargetPosition, true, hit);
            }
            else
            {
                bulletShot = this.gun.Shoot(this.bulletSpawnLocations[this.bulletSpwanLocationIndex].position,
                    crosshairWorldTargetPosition, false);
            }
            
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
                this.animationControllers[this.animationControllerIndex].SetBool("BulletFired", true);
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
        if (ctx.action.IsPressed())
        {
            this.tryingToShoot = true;
        }
        else
        {
            this.tryingToShoot = false;
        }
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
