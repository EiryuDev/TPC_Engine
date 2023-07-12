using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character; //Reference to the Character Manager Script

        int horizontal; //For Horizontal Value
        int vertical; //For Vertical Value
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();

            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }
        public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement, bool isSprinting)
        {
            //BELOW CODE: ADDING THE VALUES
            float horizontalAmount = horizontalMovement;
            float verticalAmount = verticalMovement;  

            if(isSprinting)
            {
                verticalAmount = 2;
            }

            character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
            //BELOW CODE: ADDING THE VALUES (NEW METHOD)
            //float snappedHorizontal = 0;
            //float snappedVertical = 0;

            #region Horizontal
            //BELOW CODE: CHAIN AROUND THE HORIZONTAL MOVEMENT
            //if (horizontalMovement > 0 && horizontalMovement <= 0.5f)
            //{
            //    snappedHorizontal = 0.5f;
            //}
            //else if(horizontalMovement > 0.5f && horizontalMovement <= 1)
            //{
            //    snappedHorizontal = 1;
            //}
            //else if(horizontalMovement < 0 && horizontalMovement >= -0.5f)
            //{
            //    snappedHorizontal = -0.5f;
            //}
            //else if(horizontalMovement < -0.5f && horizontalMovement >= -1)
            //{
            //    snappedHorizontal = -1;
            //}
            //else
            //{
            //    snappedHorizontal = 0;
            //}
            #endregion

            #region Vertical
            //BELOW CODE: CHAIN AROUND THE VERTICAL MOVEMENT
            //if (verticalMovement > 0 && verticalMovement <= 0.5f)
            //{
            //    snappedVertical = 0.5f;
            //}
            //else if (verticalMovement > 0.5f && verticalMovement <= 1)
            //{
            //    snappedVertical = 1;
            //}
            //else if (verticalMovement < 0 && verticalMovement >= -0.5f)
            //{
            //    verticalMovement = -0.5f;
            //}
            //else if (verticalMovement < -0.5f && verticalMovement >= -1)
            //{
            //    snappedVertical = -1;
            //}
            //else
            //{
            //    snappedVertical = 0;
            //}

            #endregion
            //character.animator.SetFloat("Horizontal", snappedHorizontal);
            //character.animator.SetFloat("Vertical", snappedVertical);
        }
        public virtual void PlayTargetActionAnimation(
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            //BELOW CODE: CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTIONS
            //BELOW CODE: EXAMPLE IF YOU GET DAMAGED AND BEGIN PERFORMING A DAMAGE ANIMATION
            //BELOW CODE: THE BELOW FLAG WILL TURN TRUE IF PLAYER IS STUNNED
            //BELOW CODE: WE CAN THEN CHECK FOR THIS FLAG BEFORE ATTEMPTING NEW ACTIONS
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;
        }
    }

}