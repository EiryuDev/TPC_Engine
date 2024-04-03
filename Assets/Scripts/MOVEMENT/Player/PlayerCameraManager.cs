using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance; // Static instance for the Player Camera Manager script
        public PlayerManager player; // Reference to the Player Manager script
        public Camera cameraObject; // Reference to the camera object component
        [SerializeField] Transform cameraPivotTransform; // Reference to transform component for camera pivot

        [Header("CAMERA SETTINGS")] // Change this to tweak camera performance
        private float cameraSmoothSpeed = 1; // Camera move smooth speed value (the bigger the number is the longer camera will reacts to the position change)
        [SerializeField] float leftAndRightRotationSpeed = 220; // Speed for left and right camera rotation
        [SerializeField] float upAndDownRotationSpeed = 220; // Speed for up and down camera rotation
        [SerializeField] float minimumPivot = -30; // The lowest pivot value where you can look down
        [SerializeField] float maximumPivot = 60; // The highest pivot value where you can look up
        [SerializeField] float cameraCollisionRadius = 0.2f; // Camera collision radius value
        [SerializeField] LayerMask collideWithLayers; // Layermask for the camera

        [Header("CAMERA VALUE")] // Display only the camera values
        private Vector3 cameraVelocity; // Camera velocity value
        private Vector3 cameraObjectPosition; // Position vector for camera object (moves the camera object to this position upon colliding)
        [SerializeField] float leftAndRightLookAngle; // Angle value for looking left and right through camera
        [SerializeField] float upAndDownLookAngle; // Angle value for looking up and down through camera
        private float defaultCameraZPosition; // Default camera z position value of the camera collisions
        private float targetCameraZPosition; // Target camera z position value of the camera collisions

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            defaultCameraZPosition = cameraObject.transform.localPosition.z;
        }
        public void UseAllCameraActions()
        {
            if(player != null)
            {
                // BELOW CODE: Follow the player
                UseFollowTarget();
                // BELOW CODE: Rotate around the player
                UseRotations();
                // BELOW CODE: Collide with objects
                UseCollisions();
            }
        }
        private void UseFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(
                transform.position,
                player.transform.position,
                ref cameraVelocity,
                cameraSmoothSpeed * Time.deltaTime);

            transform.position = targetCameraPosition;
        }
        private void UseRotations()
        {
            // BELOW CODE: If locked on, force rotation towards target
            // BELOW CODE: Else rotate normally

            // BELOW CODE: Normal rotation
            // BELOW CODE: Rotate left and right based on horizontal movement on the right joystick // mouse
            leftAndRightLookAngle += (player.playerInputManager.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            // BELOW CODE: Rotate up and down based on horizontal movement on the right joystick // mouse
            upAndDownLookAngle -= (player.playerInputManager.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            // BELOW CODE: Clamp the up and down look angle between a min and max value
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;
            // BELOW CODE: Rotate the camera left and right
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            // BELOW CODE: Rotate the camera up and down
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        private void UseCollisions()
        {
            targetCameraZPosition = defaultCameraZPosition;
            RaycastHit hit;
            // BELOW CODE: Direction for collision check
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // BELOW CODE: Check if there is object in front of camera from our above direction
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                // BELOW CODE: If there is object, get the distance from the player
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                // BELOW CODE: Then equate player's target z position to the following
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            // BELOW CODE: If player target position is less than collision radius, subtract player's collision radius (snap it back) 
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            // BELOW CODE: Apply final position using lerps
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}
