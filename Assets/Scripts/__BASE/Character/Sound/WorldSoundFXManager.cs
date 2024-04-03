using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance; // Static instance for this gameobject

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