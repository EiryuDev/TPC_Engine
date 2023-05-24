using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deceilio.TPC_Movement
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance; //Static Object for the Script
        [SerializeField] int worldSceneIndex = 1; //Choose which Index Scene to Load 
        private void Awake()
        {
            //BELOW CODE: THERE CAN ONLY BE ONE INSTANCE OF THIS SCRIPT, IF NOT AND MORE THEN DESTORY THE GAMEOBJECT
            if(instance == null)
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
            DontDestroyOnLoad(gameObject); //Let you make the GameObject run one time for all the scene as "Don't Destory on Load"
        }
        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }
        public int GetWorldSceneIndex()
        {
            return worldSceneIndex; 
        }
    }
}