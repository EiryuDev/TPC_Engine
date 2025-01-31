using UnityEditor;
using UnityEngine;

namespace Deceilio.Phaedra
{
    public class WRLD_MENU_EDITOR : MonoBehaviour
    {
        #region Player Editor
        [MenuItem("Lumimoth/RTGJ/Player/Player")]
        static void CreatePlayerObject()
        {
            InstantiatePrefab("Prefabs/Character Objects/Player Objects/Player");
        }
        
        [MenuItem("Lumimoth/RTGJ/Player/Player Camera Manager")]
        static void CreatePlayerCameraManager()
        {
            InstantiatePrefab("Prefabs/Character Objects/Player Objects/Player Managers/Player Camera Manager (Left & Right)");
        }
        
        [MenuItem("Lumimoth/RTGJ/Player/Player UI Manager")]
        static void CreatePlayerUIManagerObject()
        {
            InstantiatePrefab("Prefabs/Character Objects/Player Objects/Player Managers/Player UI Manager");
        }
        #endregion

        static void InstantiatePrefab(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogError($"Prefab not found at path: {path}");
                return;
            }

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeGameObject = instance;
        }
    }
}