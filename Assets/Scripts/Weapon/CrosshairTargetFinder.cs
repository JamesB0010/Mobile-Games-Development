using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class CrosshairTargetFinder : MonoBehaviour
{
    private class LatestHitData
    {
        public bool latestHitValid = false;
        public RaycastHit latestHit;
        public float lastValidDistance = 150.0f;

        public void UpdateData(bool latestHitValid, RaycastHit latestHit, float lastValidDistance)
        {
            this.latestHitValid = latestHitValid;
            this.latestHit = latestHit;
            this.lastValidDistance = lastValidDistance;
        }
    }

    //Attributes
    [FormerlySerializedAs("PlayerCamera")]
    [Header("Mandatory Stuff")]
    [SerializeField]
    //By eye i mean the point/transform that the target finder views/samples the world from
    private Transform EyeTransform;

    [SerializeField]
    [Tooltip("This should be an object in your scene (maybe a sphere) " +
             "that will be used to keep track of where in world space your cursor is")]
    private Transform lookingAtPoint;
    [Space]


    [Header("Configurables")]
    [SerializeField]
    private LayerMask raycastLayerMask;

    private LatestHitData latestHitData = new LatestHitData();

    private EnemyBase target;

    private bool firstPerson = true;

    private Vector2 crosshairPosition;
    //methods

    private void Update()
    {
        Vector3 cameraPosition = this.EyeTransform.position;
        bool raycastHitCollider = Physics.Raycast(cameraPosition, this.EyeTransform.forward, out RaycastHit hit, float.MaxValue, this.raycastLayerMask);
        
        
        this.SampleCrosshairTarget(cameraPosition, raycastHitCollider, hit);
    }

    private void SampleCrosshairTarget(Vector3 cameraPosition, bool raycastHitCollider, RaycastHit hit)
    {
        if (raycastHitCollider)
        {
            this.latestHitData.UpdateData(true, hit, hit.distance);
            SetLookAtPointPosition(hit);
            //Debug.Log("hit something: " + hit.collider.gameObject.name);
        }
        else
        {
            this.latestHitData.latestHitValid = false;
            InferSetLookAtPointPosition(cameraPosition);
        }

    }

    private void SetLookAtPointPosition(RaycastHit hit)
    {
        this.lookingAtPoint.transform.position = hit.point;
    }

    private void InferSetLookAtPointPosition(Vector3 cameraPosition)
    {
        this.lookingAtPoint.transform.position =
            cameraPosition + (this.EyeTransform.forward * this.latestHitData.lastValidDistance);
    }

    public Vector3 GetLatestHitPosition()
    {
        return this.lookingAtPoint.position;
    }

    public bool WasLastHitValid()
    {
        return this.latestHitData.latestHitValid;
    }

    public RaycastHit GetLastHit()
    {
        return this.latestHitData.latestHit;
    }

    public void OnCameraViewChanged(bool firstPerson)
    {
        this.firstPerson = firstPerson;
    }

    public void OnCrosshairPositionChanged(Vector2 position)
    {
        this.crosshairPosition = position;
    }
}
