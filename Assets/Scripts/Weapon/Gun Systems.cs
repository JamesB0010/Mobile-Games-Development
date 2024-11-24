using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Weapon
{
    [RequireComponent(typeof(CrosshairTargetFinder))]
    public class GunSystems : MonoBehaviour
    {
        public event Action<PlayerShipHeavyWeapon[]> HeavyGunsInitialied;
        
        //Attributes
        private CrosshairTargetFinder crosshairTargetFinder;
        public CrosshairTargetFinder CrosshairTargetFinder => this.crosshairTargetFinder;

        [SerializeField] private BoolReference aimingAtEnemy;

        private PlayerShipLightWeapon[] lightWeaponsList;

        private PlayerShipHeavyWeapon[] heavyWeaponList;

        public PlayerShipHeavyWeapon[] HeavyWeaponsList => this.heavyWeaponList;

        [SerializeField] private PlayerUpgradesState playerUpgradesState;


        public bool TryingToShootLight { get; set; } = false;

        public bool TryingToShootHeavy { get; set; } = false;

        private float timeStartedLookingAtEnemy;

        [SerializeField]
        private FloatReference timeSpentLookingAtEnemy;
        private IEnumerator Start()
        {
            this.aimingAtEnemy.SetValue(false);
            this.crosshairTargetFinder = GetComponent<CrosshairTargetFinder>();

            this.lightWeaponsList = transform.GetComponentsInChildren<PlayerShipLightWeapon>();
            this.heavyWeaponList = transform.GetComponentsInChildren<PlayerShipHeavyWeapon>();

            this.playerUpgradesState.SetPlayershipWithStoredLightWeapons(this.lightWeaponsList);
            this.playerUpgradesState.SetPlayershipWithStoredHeavyWeapons(this.heavyWeaponList);

            yield return new WaitForSeconds(0);
            this.HeavyGunsInitialied?.Invoke(this.HeavyWeaponsList);
        }

        private void Update()
        {
            if (CrosshairNotAimingAtAnythingValid())
                return;


            if (this.IsAimingAtEnemy())
            {
                this.AimingAtEnemyLogic();
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
