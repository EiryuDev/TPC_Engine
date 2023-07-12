using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager player; //Reference to the Player Manager Script
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        private void OnAnimatorMove()
        {
            if(player.applyRootMotion)
            {
                //BELOW CODE: TAKE THE ROTATION FROM THE PARTICULAR ANIMATION AND APPLY TO THE CHARACTER ROTATION
                Vector3 velocity = player.animator.deltaPosition;
                player.characterController.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }
    }
}