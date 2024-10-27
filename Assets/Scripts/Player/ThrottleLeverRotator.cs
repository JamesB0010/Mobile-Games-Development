using UnityEngine;

namespace Player
{
    public class ThrottleLeverRotator : MonoBehaviour
    {
        //Attributes
        private PlayerShipThrottle playerThrottle;

        //configurables
        [SerializeField] private float minXRotation, maxXRotation;


        private void Start()
        {
            this.playerThrottle = FindObjectOfType<PlayerShipThrottle>();
        }

        private void Update()
        {
            float throttleAmount = playerThrottle.Throttle;
            float targetXRotation = throttleAmount.MapRange(0, 1, minXRotation, maxXRotation);

            Quaternion localRotation = transform.localRotation;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(targetXRotation, localRotation.eulerAngles.y, localRotation.eulerAngles.z));

            transform.localRotation = targetRotation;
        }

    }
}
