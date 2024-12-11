using System.Collections;
using UnityEngine;

namespace Player_Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerShipEngine playerShipEngine;
        
        private Vector3 velocity = Vector3.zero;
        
        
        private float currentMaxVelocity;
        public float CurrentMaxVelocity
        {
            set => currentMaxVelocity = value;
        }


        public void OnEngineEquipped()
        {
            this.currentMaxVelocity = this.playerShipEngine.MaxVelocity;
        }

        void Update()
        {
            var acceleration = this.playerShipEngine.CalculateAcceleration();
            AddAccelerationToVelocity(acceleration);
            transform.Translate(this.velocity * Time.deltaTime, Space.World);
        }
       
        private void AddAccelerationToVelocity(Vector3 acceleration)
        {
            this.velocity += acceleration;
            this.velocity = Vector3.ClampMagnitude(this.velocity, this.currentMaxVelocity);
        }
    }
}
