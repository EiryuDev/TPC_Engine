using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Movement
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player; //Reference to the Player Manager Script

        [HideInInspector] public float verticalMovement; //Value of the Vertical Movement
        [HideInInspector] public float horizontalMovement; //Value of the Horizontal Movement
        [HideInInspector] public float moveAmount; //Value for the movement amount

        [Header("MOVEMENT SETTINGS")]
        private Vector3 moveDirection; //Directional Movement value of the Player
        private Vector3 targetRotationDirection; //Target Direction value of the player
        [SerializeField] float walkingSpeed = 2; //Value for the Walking Speed of the Player
        [SerializeField] float runningSpeed = 5; //Value for the Walking Speed of the Player
        [SerializeField] float sprintingSpeed = 6.5f; //Value for the Sprinting Speed of the Player
        [SerializeField] float rotationSpeed = 15; //Value for the Rotation Speed of the Player

        [Header("DODGE SETTINGS")]
        private Vector3 rollDirection; //Direction Value where you will roll the player
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }
        protected override void Update()
        {
            base.Update();
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.isSprinting);

        }
        public void UseAllMovement()
        {
            //BELOW CODE: GROUNDED MOVEMENT
            UseGroundedMovement();
            //BELOW CODE: PLAYER ROTATION [AERIAL MOVEMENT]
            UseRotation();
            //TO-DO: JUMPING MOVEMENT [AERIAL MOVEMENT]
            //TO-DO: PLAYER FALLING [AERIAL MOVEMENT]
        }
        private void GetMovementValues()
        {
            verticalMovement = player.playerInputManager.verticalInput;
            horizontalMovement = player.playerInputManager.horizontalInput;
            moveAmount = player.playerInputManager.moveAmount;

            //BELOW CODE: CLAMPS THE MOVEMENT
        }
        private void UseGroundedMovement()
        {
            if (!player.canMove)
                return;

            GetMovementValues();
            //BELOW CODE: MOVE DIRECTION IS BASED ON CAMERA FACING PERSPECTIVE OR MOVEMENT INPUTS
            moveDirection = PlayerCameraManager.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCameraManager.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if(player.isSprinting)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else 
            {
                if (player.playerInputManager.moveAmount > 0.5f)
                {
                    //BELOW CODE: MOVE AT A RUNNING SPEED
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (player.playerInputManager.moveAmount <= 0.5f)
                {
                    //BELOW CODE: MOVE AT A WALKING SPEED
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }
        private void UseRotation()
        {
            if (!player.canRotate)
                return;

            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCameraManager.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCameraManager.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if(targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction)
                return; //HELP TO STOP SPAMMING THE ROLL BUTTON

            //BELOW CODE: IF WE ARE MOVING WHEN WE ATTEMPT TO DODGE, WE PERFORM A ROLL
            if(player.playerInputManager.moveAmount > 0)
            {
                rollDirection = PlayerCameraManager.instance.cameraObject.transform.forward * player.playerInputManager.verticalInput;
                rollDirection += PlayerCameraManager.instance.cameraObject.transform.right * player.playerInputManager.horizontalInput;
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                //BELOW CODE: USE A ROLL ANIMATION
                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
            }
            //BELOW CODE: IF WE STATIONARY, WE PERFORM A BACKSTEP
            else
            {
                //BELOW CODE: USE A BACKSTEP ANIMATION
                player.playerAnimatorManager.PlayTargetActionAnimation("Back_Step_01", true, true);
            }
        }
        public void UseSprinting()
        {
            if(player.isPerformingAction)
            {
                //BELOW CODE: Stop Sprinting
                player.isSprinting = false;
            }

            //TO-DO: IF WE ARE OUT OF STAMINA, SET SPRINTING TO FALSE

            //BELOW CODE: PLAYER MOVING THEN SET SPRINTING TO TRUE  
            if (moveAmount >= 0.5)
            {
                player.isSprinting = true;
            }
            //BELOW CODE: PLAYER STATIONARY/MOVING SLOWLY THEN SET SPRINTING TO FALSE  
            else
            {
                player.isSprinting = false;
            }     
        }
    }
}