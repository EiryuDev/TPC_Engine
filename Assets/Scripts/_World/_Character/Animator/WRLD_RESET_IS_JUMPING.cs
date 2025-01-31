using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class WRLD_RESET_IS_JUMPING : StateMachineBehaviour
    {
        CharacterManager character; // Reference to the Character Manager Script

        // BELOW CODE: OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (character == null)
            {
                character = animator.GetComponent<CharacterManager>();
            }

            character.characterLocomotionManager.isJumping = false;
        }
    }
}