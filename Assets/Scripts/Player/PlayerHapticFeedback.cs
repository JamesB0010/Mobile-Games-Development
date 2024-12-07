using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
public class PlayerHapticFeedback : MonoBehaviour
{
    public void OnEnemyKilled(EnemyBase enemy)
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        if (distance < 187)
        {
            Debug.Log("Close range kill detected");
            HapticFeedback.HeavyFeedback();
        }
        else if (distance > 385)
        {
            Debug.Log("Long range kill detected");
            HapticFeedback.LightFeedback();
        }
        else
        {
            Debug.Log("Medium range kill detected");
            HapticFeedback.MediumFeedback();
        }
    }
}
