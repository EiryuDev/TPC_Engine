using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController; // Reference to the Character Controller componenet
        [HideInInspector] public CharacterLocomotionManager characterLocomotionManager; // Reference to the Character Locomotion Manager componenet
        [HideInInspector] public Animator animator; // Reference to the Animator componenet

        [Header("FLAGS")]
        public bool applyRootMotion = false; // Checks if the player animator having applied root motion or not
        public bool isPerformingAction = false; // Checks if the player is performing an action or not

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterLocomotionManager = GetComponent<CharacterLocomotionManager>();    
            animator = GetComponent<Animator>();        
        }
        protected virtual void LateUpdate()
        {

        }
        protected virtual void Update()
        {
            animator.SetBool("isGrounded", characterLocomotionManager.isGrounded);
        }
    }
}