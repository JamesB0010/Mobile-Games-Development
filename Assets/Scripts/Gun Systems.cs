using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CrosshairTargetFinder))]
public class GunSystems : MonoBehaviour
{
    private CrosshairTargetFinder crosshairTargetFinder;

    [SerializeField] private BoolReference aimingAtEnemy;

    public CrosshairTargetFinder CrosshairTargetFinder => this.crosshairTargetFinder;

    private Camera playerCamera;


    private bool tryingToShoot = false;
    public bool TryingToShoot => this.tryingToShoot;

    private CameraFOVChanger camFovChanger;

    [SerializeField]
    private GameObject ZoomHudElement;

    private PlayerMovement playerMovement;


    private void Start()
    {
        this.aimingAtEnemy.SetValue(false);
        this.playerCamera = FindObjectOfType<Camera>();
        this.crosshairTargetFinder = GetComponent<CrosshairTargetFinder>();
        this.camFovChanger = FindObjectOfType<CameraFOVChanger>();
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (this.crosshairTargetFinder.WasLastHitValid() == false)
        {
            this.aimingAtEnemy.SetValue(false);
            return;
        }

        if (this.crosshairTargetFinder.GetLastHit().collider == null)
        {
            this.aimingAtEnemy.SetValue(false);
            return;
        }
        bool aimingAtAnyEnemy = this.crosshairTargetFinder.GetLastHit().collider.TryGetComponent(out EnemyBase enemy);
        this.aimingAtEnemy.SetValue(aimingAtAnyEnemy);

    }

    /*private void Update()
    {
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
                if (this.playerMovement.isBoosting == false)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            this.camFovChanger.zoomCamera = true;
                            this.ZoomHudElement.SetActive(true);
                            this.playerMovement.SetSensitivityAiming();
                            
                        }
                    }
                    else
                    {
                        this.camFovChanger.zoomCamera = false; 
                        this.ZoomHudElement.SetActive(false);
                        this.playerMovement.SetSensitivityNormal();
                    }
                }
                else
                {
                    this.camFovChanger.zoomCamera = false; 
                    this.ZoomHudElement.SetActive(false);
                    this.playerMovement.SetSensitivityNormal();
                }
            }
            else
            {
                bulletShot = this.gun.Shoot(this.bulletSpawnLocations[this.bulletSpwanLocationIndex].position,
                    crosshairWorldTargetPosition, false);
                this.camFovChanger.zoomCamera = false; 
                this.ZoomHudElement.SetActive(false);
                this.playerMovement.SetSensitivityNormal();
            }
        }
    }*/

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
