using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deceilio.TPC_Engine
{
    public class PlayerInputManager : MonoBehaviour
    {
        public PlayerManager player; //Reference to the Player Manager Script
        PlayerControls playerControls; //Reference to the Player Controls Actions File for ref player Input

        [Header("CAMERA INPUT")]
        [SerializeField] Vector2 cameraInput; //Storing the Data for referencing for the Input Values for Camera
        public float cameraVerticalInput; //Vertical Input value for the camera
        public float cameraHorizontalInput; //Horizontal Input value for the camera

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput; //Storing the Data for referencing for the Input Values for movement
        public float verticalInput; //Vertical Input value for the movement
        public float horizontalInput; //Horizontal Input value for the movement
        public float moveAmount; //Move Amount value for the player

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false; //Check if the Input for the dodge/roll is pressed or not
        [SerializeField] bool sprintInput = false; //Check if the Input for the sprint is pressed or not
        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        //BELOW CODE: READ THE VALUES OF JOYSTICK/KEYBOARD
        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;

                //BELOW CODE: HOLDING THE SPRINT INPUT, MAKE BOOL TRUE
                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                //BELOW CODE: RELEASING THE SPRINT INPUT, MAKE BOOL FALSE
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
            }

            playerControls.Enable();
        }

        //BELOW CODE: IF PLAYER MINIMIZE OR LOWER THE WINDOW, STOP ADJUSTING INPUTS
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
            UseSprinting();
        }

        //BELOW CODE: MOVE THE CHARACTER BASED ON THE VALUES // PLAYER MOVEMENT
        private void UsePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            //BELOW CODE: RETURNS THE ABSOLUTE NUMBER (WITHOUT NEGATIVE NUMBERS, SO THE VALUE IS ALWAYS POSITIIVE)
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            //BELOW CODE: CLAMPING THE VALUES SO IT WILL BE ALWAYS 0, 0.5 & 1
            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if(moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            //BELOW CODE: WE PASS 0 TO HORIZONTAL BECAUSE WE ONLY WANT NON-STRAFING MOVEMENT
            //LOGIC: WE USE THE HORIZONTAL WHEN WE STRAFING OR LOCKED ON

            if (player == null)
                return;

            //BELOW CODE: IF WE ARE NOT LOCKED ON, ONLY USE THE DEFAULT MOVEMENT
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.isSprinting);

            //TO-DO: IF WE ARE LOCKED ON, PASS THE ADDITIONAL MOVEMENT AS WELL
        }
        private void UseCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;  
            cameraHorizontalInput = cameraInput.x;
        }

        //BELOW CODE: PLAYER ACTIONS
        private void UseDodgeInput()
        {
            if(dodgeInput)
            {
                dodgeInput = false;

                //TO-DO: DO NOTHING IF MENU OR UI WINDOW IS OPEN
                //BELOW CODE: PERFORM A DODGE
                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }    
        private void UseSprinting()
        {
            if(sprintInput)
            {
                //BELOW CODE: Player started Sprinting
                player.playerLocomotionManager.UseSprinting();
            }
            else
            {
                player.isSprinting = false;
            }
        }
    }
}