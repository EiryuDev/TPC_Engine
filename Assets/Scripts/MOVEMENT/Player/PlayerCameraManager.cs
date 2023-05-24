using UnityEngine;

namespace Deceilio.TPC_Movement
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance; //Static Instance for the Player Camera Manager Script
        public PlayerManager player; //Reference to the Player Manager Script
        public Camera cameraObject; //Reference to the Camera Object Component
        [SerializeField] Transform cameraPivotTransform; //Reference to Transform Component for Camera Pivot

        [Header("CAMERA SETTINGS")] //Change this to tweak camera performance
        private float cameraSmoothSpeed = 1; //Camera Move Smooth Speed Value (The Bigger the number is the longer camera will reacts to the position change)
        [SerializeField] float leftAndRightRotationSpeed = 220; //Speed for Left and Right camera rotation
        [SerializeField] float upAndDownRotationSpeed = 220; //Speed for Up and Down camera rotation
        [SerializeField] float minimumPivot = -30; //The Lowest Pivot value where you can look down
        [SerializeField] float maximumPivot = 60; //The Highest Pivot value where you can look up
        [SerializeField] float cameraCollisionRadius = 0.2f; //Camera Collision Radius Value
        [SerializeField] LayerMask collideWithLayers; //LayerMask for the Camera

        [Header("CAMERA VALUE")] //Display only the Camera Values
        private Vector3 cameraVelocity; //Camera Velocity Value
        private Vector3 cameraObjectPosition; //Position Vector for Camera Object (Moves the Camera Object to this position upon colliding)
        [SerializeField] float leftAndRightLookAngle; //Angle value for looking left and right through camera
        [SerializeField] float upAndDownLookAngle; //Angle value for looking up and down through camera
        private float defaultCameraZPosition; //Default Camera Z Position Value of the Camera Collisions
        private float targetCameraZPosition; //Target Camera Z Position Value of the Camera Collisions
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
                //BELOW CODE: FOLLOW THE PLAYER
                UseFollowTarget();
                //BELOW CODE: ROTATE AROUND THE PLAYER
                UseRotations();
                //BELOW CODE: COLLIDE WITH OBJECTS
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
            //BELOW CODE: IF LOCKED ON, FORCE ROTATION TOWARDS TARGET
            //BELOW CODE: ELSE ROTATE NORMALLY

            //BELOW CODE: NORMAL ROTATION
            //BELOW CODE: ROTATE LEFT AND RIGHT BASED ON HORIZONTAL MOVEMENT ON THE RIGHT JOYSTICK // MOUSE
            leftAndRightLookAngle += (player.playerInputManager.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            //BELOW CODE: ROTATE UP AND DOWN BASED ON HORIZONTAL MOVEMENT ON THE RIGHT JOYSTICK // MOUSE
            upAndDownLookAngle -= (player.playerInputManager.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            //BELOW CODE: CLAMP THE UP AND DOWN LOOK ANGLE BETWEEN A MIN AND MAX VALUE
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;
            //BELOW CODE: ROTATE THE CAMERA LEFT AND RIGHT
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            //BELOW CODE: ROTATE THE CAMERA UP AND DOWN
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        private void UseCollisions()
        {
            targetCameraZPosition = defaultCameraZPosition;
            RaycastHit hit;
            //BELOW CODE: DIRECTION FOR COLLISION CHECK
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            //BELOW CODE: CHECK IF THERE IS OBJECT IN FRONT OF CAMERA FROM OUR ABOVE DIRECTION
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                //BELOW CODE: IF THERE IS OBJECT, GET THE DISTANCE FROM THE PLAYER
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                //BELOW CODE: THEN EQUATE PLAYER'S TARGET Z POSITION TO THE FOLLOWING
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            //BELOW CODE: IF PLAYER TARGET POSITION IS LESS THAN COLLISION RADIUS, SUBTRACT PLAYER'S COLLISION RADIUS (SNAP IT BACK) 
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            //BELOW CODE: APPLY FINAL POSITION USING LERP
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}
