using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deceilio.TPC_Engine
{
    public class WRLD_SAVE_GAME_MANAGER : MonoBehaviour
    {
        public static WRLD_SAVE_GAME_MANAGER instance; // Static object for the script
        [SerializeField] int worldSceneIndex = 1; // Choose which index scene to load 
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
            DontDestroyOnLoad(gameObject); // Let you make the GameObject run one time for all the scene as "Don't Destory on Load"
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