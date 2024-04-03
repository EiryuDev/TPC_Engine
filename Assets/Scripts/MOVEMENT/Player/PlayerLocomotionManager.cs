using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player; // Reference to the Player Manager script

        [HideInInspector] public float verticalMovement; // Value of the vertical movement
        [HideInInspector] public float horizontalMovement; // Value of the horizontal movement
        [HideInInspector] public float moveAmount; // Value for the movement amount

        [Header("MOVEMENT SETTINGS")]
        private Vector3 moveDirection; // Directional movement value of the player
        private Vector3 targetRotationDirection; // Target direction value of the player
        [SerializeField] float walkingSpeed = 2; // Value for the walking speed of the player
        [SerializeField] float runningSpeed = 5; // Value for the walking speed of the player
        [SerializeField] float sprintingSpeed = 6.5f; // Value for the sprinting speed of the player
        [SerializeField] float rotationSpeed = 15; // Value for the rotation speed of the player

        [Header("JUMP")]
        [SerializeField] private float jumpStaminaCost = 5; // Stamina cost value for reduction of stamina after jumping 
        [SerializeField] float jumpHeight = 4; // Jump height value for the player
        [SerializeField] float jumpForwardSpeed = 5; // Forward jump speed for the player
        [SerializeField] float freeFallSpeed = 2; // Free fall speed for the player
        private Vector3 jumpDirection; // Direction value where you will jump the player

        [Header("DODGE SETTINGS")]
        private Vector3 rollDirection; // Direction value where you will roll the player
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }
        protected override void Update()
        {
            base.Update();
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, isSprinting);

        }
        public void UseAllMovement()
        {
            // BELOW CODE: Grounded movement
            UseGroundedMovement();
            // BELOW CODE: Player rotation [aerial movement]
            UseRotation();
            // TO-DO: Jumping movement [aerial movement]
            UseJumpingMovement();
            // TO-DO: Player falling [aerial movement]
            UseFreeFallMovement();
        }
        private void GetMovementValues()
        {
            verticalMovement = player.playerInputManager.verticalInput;
            horizontalMovement = player.playerInputManager.horizontalInput;
            moveAmount = player.playerInputManager.moveAmount;

            // BELOW CODE: Clamps the movement
        }
        private void UseGroundedMovement()
        {
            if (!canMove)
                return;

            GetMovementValues();
            // BELOW CODE: Move direction is based on camera facing perspective or movement inputs
            moveDirection = PlayerCameraManager.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCameraManager.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if(isSprinting)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else 
            {
                if (player.playerInputManager.moveAmount > 0.5f)
                {
                    // BELOW CODE: Move at a running speed
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (player.playerInputManager.moveAmount <= 0.5f)
                {
                    // BELOW CODE: Move at a walking speed
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }
        private void UseJumpingMovement()
        {
            if (isJumping)
            {
                player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
            }
        }
        private void UseFreeFallMovement()
        {
            if (!isGrounded)
            {
                Vector3 freeFallDirection;
                freeFallDirection = PlayerCameraManager.instance.transform.forward * player.playerInputManager.verticalInput;
                freeFallDirection = freeFallDirection + PlayerCameraManager.instance.transform.right * player.playerInputManager.horizontalInput;
                freeFallDirection.y = 0;

                player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
            }
        }
        private void UseRotation()
        {
            if (!canRotate)
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
            if (!canDodge)
                return;

            if (player.isPerformingAction)
                return; // Help to stop spamming the roll button

            // BELOW CODE: If we are moving when we attempt to dodge, we perform a roll
            if (player.playerInputManager.moveAmount > 0)
            {
                rollDirection = PlayerCameraManager.instance.cameraObject.transform.forward * player.playerInputManager.verticalInput;
                rollDirection += PlayerCameraManager.instance.cameraObject.transform.right * player.playerInputManager.horizontalInput;
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                // BELOW CODE: Use a roll animation
                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
            }
            // BELOW CODE: If we stationary, we perform a backstep
            else
            {
                // BELOW CODE: Use a backstep animation
                player.playerAnimatorManager.PlayTargetActionAnimation("Back_Step_01", true, true);
            }
        }
        public void AttemptToPerformJump()
        {
            if (!canJump)
                return;

            // BELOW CODE: If performing an action, we don't want player to jump (need to change when adding combat)
            if (player.isPerformingAction)
                return; // Help to stop spamming the roll button

            // BELOW CODE: If we already jumping, we don't want to allow tp jump again until it finishes
            if (isJumping)
                return;

            // BELOW CODE: If we are not grounded, we don't want player to jumps
            if (!isGrounded)
                return;

            // BELOW CODE: If 2 handing weapon, play the 2 hand animations, otherwise play the one handed animation
            player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_Start_01", false);
            isJumping = true;

            jumpDirection = PlayerCameraManager.instance.cameraObject.transform.forward * player.playerInputManager.verticalInput;
            jumpDirection += PlayerCameraManager.instance.cameraObject.transform.right * player.playerInputManager.horizontalInput;
            jumpDirection.y = 0;

            if (jumpDirection != Vector3.zero)
            {
                // BELOW CODE: If player is sprinting, jump direction is at full distance
                if (isSprinting)
                {
                    jumpDirection *= 1;
                }
                // BELOW CODE: If player is running, jump direction is at half distance
                else if (player.playerInputManager.moveAmount > 0.5)
                {
                    jumpDirection *= 0.5f;
                }
                // BELOW CODE: If player is walking, jump direction is at quarter distance
                else if (player.playerInputManager.moveAmount <= 0.5)
                {
                    jumpDirection *= 0.25f;
                }
            }

        }
        public void ApplyJumpVelocity()
        {
            // BELOW CODE: Apply an upward velocity
            yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
        }
        public void UseSprinting()
        {
            if(player.isPerformingAction)
            {
                // BELOW CODE: Stop Sprinting
                isSprinting = false;
            }

            // TO-DO: If we are out of stamina, set sprinting to false

            // BELOW CODE: Player moving then set sprinting to true  
            if (moveAmount >= 0.5)
            {
                isSprinting = true;
            }
            // BELOW CODE: Player stationary/moving slowly then set sprinting to false  
            else
            {
                isSprinting = false;
            }     
        }
    }
}