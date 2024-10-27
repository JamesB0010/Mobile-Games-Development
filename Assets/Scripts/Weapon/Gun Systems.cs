using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    [RequireComponent(typeof(CrosshairTargetFinder))]
    public class GunSystems : MonoBehaviour
    {
        //Attributes
        private CrosshairTargetFinder crosshairTargetFinder;
        public CrosshairTargetFinder CrosshairTargetFinder => this.crosshairTargetFinder;

        [SerializeField] private BoolReference aimingAtEnemy;

        [SerializeField] private PlayerShipWeapon[] weaponsList;

        [SerializeField] private PlayerWeaponsState playerWeaponsState;


        private bool tryingToShoot = false;
        public bool TryingToShoot => this.tryingToShoot;

        private float timeStartedLookingAtEnemy;

        [SerializeField]
        private FloatReference timeSpentLookingAtEnemy;
        private void Start()
        {
            this.aimingAtEnemy.SetValue(false);
            this.crosshairTargetFinder = GetComponent<CrosshairTargetFinder>();
        
            this.playerWeaponsState.SetPlayershipWithStoredWeapons(this.weaponsList);
        }

        private void Update()
        {
            if (CrosshairNotAimingAtAnythingValid()) 
                return;
        
        
            bool aimingAtAnyEnemy = IsAimingAtEnemy();
            if (aimingAtAnyEnemy)
            {
                AimingAtEnemyLogic();
            }
        }

        private void AimingAtEnemyLogic()
        {
            bool wasntAimingAtEnemyLastFrame = Convert.ToBoolean(this.aimingAtEnemy.GetValue()) == false;
            if (wasntAimingAtEnemyLastFrame)
            {
                //started looking at enemy
                this.timeStartedLookingAtEnemy = Time.timeSinceLevelLoad;
            }

            this.aimingAtEnemy.SetValue(true);
            this.timeSpentLookingAtEnemy.SetValue(Time.timeSinceLevelLoad - this.timeStartedLookingAtEnemy);
        }

        private bool IsAimingAtEnemy()
        {
            return this.crosshairTargetFinder.GetLastHit().collider.TryGetComponent(out EnemyBase enemy);
        }

        private bool CrosshairNotAimingAtAnythingValid()
        {
            //Aiming at nothing
            if (this.crosshairTargetFinder.WasLastHitValid() == false)
            {
                this.aimingAtEnemy.SetValue(false);
                return true;
            }

            //aiming at something which no longer exists
            //(this occurs when you destroy an enemy, until you look at something else the last thing you looked at (the enemy) doesnt exist)
            if (this.crosshairTargetFinder.GetLastHit().collider == null)
            {
                this.aimingAtEnemy.SetValue(false);
                return true;
            }

            return false;
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
}
