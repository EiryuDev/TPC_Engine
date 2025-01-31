using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource; // Reference to the Audio Source component

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WRLD_SOUND_FX_MANAGER.instance.rollSFX);
        }
    }
}