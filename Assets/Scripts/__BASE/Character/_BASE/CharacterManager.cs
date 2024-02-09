using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController; //Reference to the Character Controller Componenet
        [HideInInspector] public Animator animator; //Reference to the Animator Component

        [Header("FLAGS")]
        public bool isPerformingAction = false; //Checks if the player is performing an action or not
        public bool applyRootMotion = false; //Checks if the player animator having applied root motion or not
        public bool canRotate = true; //Checks if the player can rotate or not
        public bool canMove = true; //Checks if the player can move or not

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();        
        }
        protected virtual void LateUpdate()
        {

        }
        protected virtual void Update()
        {

        }
    }
}