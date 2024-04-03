using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class ResetAnimationFlags : StateMachineBehaviour
    {
        CharacterManager character; // Reference to the Character Manager script

        // BELOW CODE: OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(character == null)
            {
                character = animator.GetComponent<CharacterManager>();
            }

            // BELOW CODE: This is called when actions ends, and the states return to "Empty"
            character.isPerformingAction = false;
            character.applyRootMotion = false;
            character.canRotate = true;
            character.canMove = true;
        }
    }
}