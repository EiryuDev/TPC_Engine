using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Movement
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource; //Reference to the Audio Source Component

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
        }
    }
}