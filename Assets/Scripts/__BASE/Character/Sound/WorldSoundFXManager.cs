using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance; //Static Instance for this GameObject

        [Header("ACTION SOUNDS")]
        public AudioClip rollSFX; //Sound Effect for Character Rolling

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}