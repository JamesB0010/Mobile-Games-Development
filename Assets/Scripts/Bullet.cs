using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public Vector3 forwards;

    [SerializeField] private float distanceAwayFromImpactToSpawnImpactPrefab;

    [SerializeField]
    private Transform BulletImpactPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.forwards * (this.speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collided");

        Instantiate(this.BulletImpactPrefab, transform.position + (-this.forwards * this.distanceAwayFromImpactToSpawnImpactPrefab), Quaternion.identity);

        Destroy(this.gameObject);
    }

}
