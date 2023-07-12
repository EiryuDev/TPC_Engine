using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance; //Static Object for the Script

        private void Awake()
        {
            //BELOW CODE: THERE CAN ONLY BE ONE INSTANCE OF THIS SCRIPT, IF NOT AND MORE THEN DESTORY THE GAMEOBJECT
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