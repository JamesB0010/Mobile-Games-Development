using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("Mandatory Stuff")]
    [SerializeField]
    private Camera PlayerCamera;

    [SerializeField]
    [Tooltip("This should be an object in your scene (maybe a sphere) " +
             "that will be used to keep track of where in world space your cursor is")]
    private Transform lookingAtPoint;
    [Space]


    [Header("Configurables")]
    [SerializeField]
    private LayerMask raycastLayerMask;

    private LatestHitData latestHitData = new LatestHitData();

    //methods

    private void Update()
    {
        this.SampleCrosshairTarget();
    }

    public void SampleCrosshairTarget()
    {
        Vector3 cameraPosition = this.PlayerCamera.transform.position;

        bool raycastHitCollider = Physics.Raycast(cameraPosition, this.PlayerCamera.transform.forward, out RaycastHit hit, float.MaxValue,
            this.raycastLayerMask);


        if (raycastHitCollider)
        {
            this.latestHitData.UpdateData(true, hit, hit.distance);
            SetLookAtPointPosition(hit);
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
            cameraPosition + (this.PlayerCamera.transform.forward * this.latestHitData.lastValidDistance);
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
}
