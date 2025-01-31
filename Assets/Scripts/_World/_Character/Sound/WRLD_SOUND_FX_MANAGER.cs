using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class WRLD_SOUND_FX_MANAGER : MonoBehaviour
    {
        public static WRLD_SOUND_FX_MANAGER instance; // Static instance for this gameobject

        [Header("ACTION SOUNDS")]
        public AudioClip rollSFX; // Sound effect for character rolling

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