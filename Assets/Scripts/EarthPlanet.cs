using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class EarthPlanet : MonoBehaviour
{
    private PlayerMovement player;

    private Vector3 distanceToStayAwayFromPlayer;

    private IssApiJsonResponse issPosition;

    [SerializeField] private Transform issObject;

    void Start()
    {
        this.player = FindObjectOfType<PlayerMovement>();
        this.distanceToStayAwayFromPlayer = transform.position - this.player.transform.position;

        PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
        StartCoroutine(nameof(RequestIssPosition));
    }
    
    [Serializable]
    struct IssApiJsonResponse
    {
        [Serializable]
        public struct Iss_position
        {
            public float latitude;
            public float longitude;
        }

        public Iss_position iss_position;
        
        public float timestamp;
    }

    //chat gpt
    private void PlaceISSTransform()
    {
    }


    
    IEnumerator RequestIssPosition()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://api.open-notify.org/iss-now.json"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            
            Debug.Log("Response recieved");

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                this.issPosition = JsonUtility.FromJson<IssApiJsonResponse>(webRequest.downloadHandler.text);
                this.issPosition.timestamp = Time.timeSinceLevelLoad;
                Debug.Log("Iss position Latitude " + this.issPosition.iss_position.latitude);
                Debug.Log("iss position longitude " + this.issPosition.iss_position.longitude);
                Debug.Log("Time Since level loaded " + this.issPosition.timestamp);
                
                this.PlaceISSTransform();
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.player.transform.position + this.distanceToStayAwayFromPlayer;
    }
}
