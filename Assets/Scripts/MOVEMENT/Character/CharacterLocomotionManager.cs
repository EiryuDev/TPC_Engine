using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character; // Reference to the Character Manager script

        [Header("FLAGS")]
        public bool isGrounded = true; // Checks if the player is grounded or not
        public bool isSprinting = false; // Checks if the player is sprinting or not  
        public bool isJumping = false; // Checks if the player is jumping or not
        public bool canRotate = true; // Checks if the player can rotate or not
        public bool canMove = true; // Checks if the player can move or not
        public bool canDodge = true; // Checks if the player can jump or not
        public bool canJump = true; // Checks if the player can jump or not

        [Header("GROUND CHECK & JUMPING")]
        [SerializeField] protected float gravityForce = -40; // Gravity force for the jumping
        [SerializeField] LayerMask groundLayer; // Layer mask for the ground
        [SerializeField] float groundCheckSphereRadius = 0.3f; // Radius of the sphere for the ground check
        [SerializeField] protected Vector3 yVelocity; // The force at which our character is pulled up or down (jumping or falling)
        [SerializeField] protected float groundedYVelocity = -20; // The force at which our character is sticking to the ground while they are grounded
        [SerializeField] private protected float fallStartYVelocity = -5; // The force at which our character begins to fall when they are ungrounded (rises as they fall longer)
        protected bool fallingVelocityHasBeenSet = false; // Checks if the falling velocity has been set or not
        protected float inAirTimer = 0; // Timer to depect the character in the air
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        protected virtual void Update()
        {
            UseGroundCheck();

            if (isGrounded)
            {
                // BELOW CODE: If character is not attempting to jump or move upward 
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                // BELOW CODE: If character is not jumping, and character falling velocity has not been set
                if (isJumping && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer = inAirTimer + Time.deltaTime;
                character.animator.SetFloat("inAirTimer", inAirTimer);

                yVelocity.y += gravityForce * Time.deltaTime;
            }

            // BELOW CODE: There should always be some force applied to the y velocity
            character.characterController.Move(yVelocity * Time.deltaTime);
        }
        protected void UseGroundCheck()
        {
            isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        }

        // BELOW CODE: Draws character check sphere in scene view
        protected void OnDrawGizmosSelected()
        {
            //Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        }
        public void EnableCanRotate()
        {
            canRotate = true;
        }
        public void DisableCanRotate()
        {
            canRotate = false;
        }
    }
}