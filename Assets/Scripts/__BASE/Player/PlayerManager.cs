using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager; // Reference to the Player Animator Manager script
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager; // Reference to the Player Locomotion Manager script
        [HideInInspector] public PlayerInputManager playerInputManager; // Reference to the Player Input Manager script

        protected override void Awake()
        {
            base.Awake();
            playerInputManager = GetComponent<PlayerInputManager>();    
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();  
        }
        protected override void LateUpdate()
        {
            base.LateUpdate();  

            PlayerCameraManager.instance.UseAllCameraActions();
        }
        protected override void Update()
        {
            base.Update();

            // BELOW CODE: Use/handle all player movements
            playerLocomotionManager.UseAllMovement();
        }
    }
}