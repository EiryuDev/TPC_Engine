using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Movement
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager; //Reference to the Player Animator Manager Script
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager; //Reference to the Player Locomotion Manager Script

      [Header("FLAGS")]
        public bool isSprinting = false; //Checks if the player is sprinting or not  
        protected override void Awake()
        {
            base.Awake();
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

            //BELOW CODE: USE/HANDLE ALL THE PLAYER MOVEMENTS
            playerLocomotionManager.UseAllMovement();
        }
    }
}