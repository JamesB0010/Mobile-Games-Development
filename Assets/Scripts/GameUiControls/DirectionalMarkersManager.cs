using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalMarkersManager : MonoBehaviour
{
    private List<EnemyBase> enemies;

    public List<EnemyBase> Enemies
    {
        set => this.enemies = value;
    }

    [SerializeField] private Image directionMarkerPrefab;

    private Dictionary<EnemyBase, Image> DirectionMarkers = new();

    [SerializeField] private PlayerCamsManager playerCamsManager;

    [SerializeField] private Transform playerTransform;
    
    public void RoundSpawned()
    {
        Debug.Log("Create new direction markers");

        foreach (EnemyBase enemy in enemies)
        {
            //parent in this instance is the players main canvas
            Image directionalMarker = Instantiate(directionMarkerPrefab, transform.parent);
            this.DirectionMarkers.Add(enemy, directionalMarker);
        }
    }

    private void Update()
    {
        this.PositionAndOrientMarkers();
        this.SetMarkerOpacities();
    }

    private void OrientMarkers()
    {
        foreach (KeyValuePair<EnemyBase, Image> marker in this.DirectionMarkers)
        {
            
        }
    }

    private void SetMarkerOpacities()
    {
        foreach (KeyValuePair<EnemyBase, Image> marker in this.DirectionMarkers)
        {
            Vector3 directionToEnemy = (marker.Key.transform.position - this.playerTransform.position).normalized;

            Vector3 lookDirection = this.playerCamsManager.GetActiveCamera().transform.forward;

            float lookingAtEnemyAmount = Vector3.Dot(directionToEnemy, lookDirection);

            if (lookingAtEnemyAmount < 0)
                lookingAtEnemyAmount = 0;

            var markerColor = marker.Value.color;
            marker.Value.color = new Color(markerColor.r, markerColor.g, markerColor.b, 1 - lookingAtEnemyAmount);
        }
    }

    private void PositionAndOrientMarkers()
    {
        float distanceFromCenterOfScreen = Screen.width * 0.1f;

        for (int i = 0; i < this.enemies.Count; i++)
        {
            Image marker = this.DirectionMarkers[this.enemies[i]];

            Vector3 screenSpaceEnemy = this.playerCamsManager.GetActiveCamera()
                .WorldToScreenPoint(this.enemies[i].transform.position);

            if (screenSpaceEnemy.z < 0)
                // If behind the camera, flip it to appear on the screen
                screenSpaceEnemy *= -1;
            
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenSpaceEnemy - screenCenter;

            Vector3 screenSpaceDirectionNormalised = direction.normalized;
            Vector3 positionForMarker = screenCenter + screenSpaceDirectionNormalised * distanceFromCenterOfScreen;

            // Clamp the position to the screen edges
            positionForMarker.x = Mathf.Clamp(positionForMarker.x, 0, Screen.width);
            positionForMarker.y = Mathf.Clamp(positionForMarker.y, 0, Screen.height);

            
            marker.rectTransform.anchoredPosition = positionForMarker - screenCenter;

            OrientMarker(direction, marker);
        }

    }

    private static void OrientMarker(Vector3 direction, Image marker)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        marker.rectTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnEnemyKilled(EnemyBase enemy)
    {
        this.DirectionMarkers.Remove(enemy, out Image image);
        
        Destroy(image);
    }
}
