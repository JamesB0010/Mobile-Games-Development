using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipWeapon : MonoBehaviour
{
    private Gun gun;

    [SerializeField] private AudioSource gunshotSoundLocation;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform bulletSpawnLocation;
    
    
}
