using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Movement
{
    public class ResetAnimationFlags : StateMachineBehaviour
    {
        CharacterManager character; //Reference to the Character Manager Script

        //BELOW CODE: OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(character == null)
            {
                character = animator.GetComponent<CharacterManager>();
            }

            //BELOW CODE: THIS IS CALLED WHEN ACTIONS ENDS, AND THE STATES RETURN TO "EMPTY"
            character.isPerformingAction = false;
            character.applyRootMotion = false;
            character.canRotate = true;
            character.canMove = true;
        }
    }
}