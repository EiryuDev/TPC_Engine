using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance; // Static object for the script

        private void Awake()
        {
            // BELOW CODE: There can only be one instance of this script, if not and more then destory the gameobject
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);  
        }
    }
}