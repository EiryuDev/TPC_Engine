using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerInputManager : MonoBehaviour
    {
        public PlayerManager player; // Reference to the Player Manager script
        PlayerControls playerControls; // Reference to the player controls actions file for ref player input

        [Header("CAMERA INPUT")]
        [SerializeField] Vector2 cameraInput; // Storing the data for referencing for the input values for camera
        public float cameraVerticalInput; // Vertical input value for the camera
        public float cameraHorizontalInput; // Horizontal input value for the camera

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput; // Storing the data for referencing for the input values for movement
        public float verticalInput; // Vertical input value for the movement
        public float horizontalInput; // Horizontal input value for the movement
        public float moveAmount; // Move amount value for the player

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false; // Check if the input for the dodge/roll is pressed or not
        [SerializeField] bool sprintInput = false; // Check if the input for the sprint is pressed or not
        [SerializeField] bool jumpInput = false; // Check if the input for the jump is pressed or not
        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        // BELOW CODE: Read the values of Joystick/Keyboard
        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

                // Actions
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

                // BELOW CODE: Holding the sprint input, make bool true
                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                // BELOW CODE: Releasing the sprint input, make bool false
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
            }

            playerControls.Enable();
        }

        // BELOW CODE: If player minimize or lower the window, stop adjusting inputs
        private void OnApplicationFocus(bool focus)
        {
            if(enabled)
            {
                if(focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }
        private void Update()
        {
            UseAllInputs();
        }
        private void UseAllInputs()
        {
            UsePlayerMovementInput();
            UseCameraMovementInput();
            UseDodgeInput();
            UseJumpInput();
            UseSprintInput();
        }

        // BELOW CODE: Move the character based on the values // player movement
        private void UsePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            // BELOW CODE: Returns the absolute number (without negative numbers, so the value is always positive)
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // BELOW CODE: Clamping the values so it will be always 0, 0.5 & 1
            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if(moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            // BELOW CODE: We pass 0 to horizontal because we only want non-strafing movement
            // LOGIC: We use the horizontal when we strafing or locked on

            if (player == null)
                return;

            // BELOW CODE: If we are not locked on, only use the default movement
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerLocomotionManager.isSprinting);

            // TO-DO: If we are locked on, pass the additional movement as well
        }
        private void UseCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;  
            cameraHorizontalInput = cameraInput.x;
        }

        // BELOW CODE: Player actions
        private void UseDodgeInput()
        {
            if(dodgeInput)
            {
                dodgeInput = false;

                // TO-DO: Do nothing if menu or ui window is open
                // BELOW CODE: Perform a dodge
                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }
        private void UseJumpInput()
        {
            if (jumpInput)
            {
                jumpInput = false;

                // BELOW CODE: If as UI window opened, simply return wihout doing anything
                // BELOW CODE: Attempt to perform jump
                player.playerLocomotionManager.AttemptToPerformJump();
            }
        }
        private void UseSprintInput()
        {
            if(sprintInput)
            {
                // BELOW CODE: Player started sprinting
                player.playerLocomotionManager.UseSprinting();
            }
            else
            {
                player.playerLocomotionManager.isSprinting = false;
            }
        }
    }
}