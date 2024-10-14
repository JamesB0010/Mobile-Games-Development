using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public Vector3 forwards;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.forwards * (this.speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collided");

        Destroy(this.gameObject);
    }

}
