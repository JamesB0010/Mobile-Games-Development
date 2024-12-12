using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
public class PlayerHapticFeedback : MonoBehaviour
{
    [SerializeField] private HapticFeedbackObject hapticFeedback;
    
    public void OnEnemyKilled(EnemyBase enemy)
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        if (distance < 187)
        {
            Debug.Log("Close range kill detected");
            this.hapticFeedback.HeavyVibration();
        }
        else if (distance > 385)
        {
            Debug.Log("Long range kill detected");
            this.hapticFeedback.LightVibration();
        }
        else
        {
            Debug.Log("Medium range kill detected");
            this.hapticFeedback.MediumVibration();
        }
    }
}
