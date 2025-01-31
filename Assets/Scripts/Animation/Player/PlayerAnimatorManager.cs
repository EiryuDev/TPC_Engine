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
                // BELOW CODE: Take the rotation from the particular animation and apply to the character rotation
                Vector3 velocity = player.animator.deltaPosition;
                player.characterController.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }
    }
}