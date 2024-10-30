using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Weapon
{
    [RequireComponent(typeof(CrosshairTargetFinder))]
    public class GunSystems : MonoBehaviour
    {
        //Attributes
        private CrosshairTargetFinder crosshairTargetFinder;
        public CrosshairTargetFinder CrosshairTargetFinder => this.crosshairTargetFinder;

        [SerializeField] private BoolReference aimingAtEnemy;

        private PlayerShipLightWeapon[] lightWeaponsList;

        private PlayerShipHeavyWeapon[] heavyWeaponList;

        [SerializeField] private PlayerWeaponsState playerWeaponsState;


        public bool tryingToShootLightLight = false;
        public bool TryingToShootLight
        {
            get => this.tryingToShootLightLight;

            set => this.tryingToShootLightLight = value;
        }

        public bool tryingToShootHeavy = false;

        public bool TryingToShootHeavy
        {
            get => this.tryingToShootHeavy;
            set => this.tryingToShootHeavy = value;
        }

        private float timeStartedLookingAtEnemy;

        [SerializeField]
        private FloatReference timeSpentLookingAtEnemy;
        private void Start()
        {
            this.aimingAtEnemy.SetValue(false);
            this.crosshairTargetFinder = GetComponent<CrosshairTargetFinder>();

            this.lightWeaponsList = transform.GetComponentsInChildren<PlayerShipLightWeapon>();
            this.heavyWeaponList = transform.GetComponentsInChildren<PlayerShipHeavyWeapon>();

            this.playerWeaponsState.SetPlayershipWithStoredLightWeapons(this.lightWeaponsList);
            this.playerWeaponsState.SetPlayershipWithStoredHeavyWeapons(this.heavyWeaponList);
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
    }
}
