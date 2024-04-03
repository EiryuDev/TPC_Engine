
using UnityEditor;
using UnityEngine;

namespace Deceilio.Phaedra
{
    public class WRLD_PHAEDRA_MENU_EDITOR : MonoBehaviour
    {
        #region Player Editor
        [MenuItem("Deceilio/Phaedra/Player/Player Camera")]
        static void CreatePlayerCamera()
        {
            var go = Resources.Load("Prefabs/Player Objects/PlayerCamera");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }

        [MenuItem("Deceilio/Phaedra/Player/Main Player")]
        static void CreatePlayerObject()
        {
            var go = Resources.Load("Prefabs/Player Objects/Player");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }

        [MenuItem("Deceilio/Phaedra/Player/Player UI")]
        static void CreatePlayerUI()
        {
            var go = Resources.Load("Prefabs/Player Objects/PlayerUI");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }
        #endregion

        #region Enemy Editor
        [MenuItem("Deceilio/Phaedra/Enemies/Mob Beta/Normal AI")]
        static void CreateMobBetaNormal()
        {
            var go = Resources.Load("Prefabs/Enemy Objects/MobBeta(Normal)");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }

        [MenuItem("Deceilio/Phaedra/Enemies/Mob Beta/Advance AI")]
        static void CreateMobBetaAdvance()
        {
            var go = Resources.Load("Prefabs/Enemy Objects/MobBeta(Advance)");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }
        #endregion

        #region Menus
        [MenuItem("Deceilio/Phaedra/Menus/Leveling Up")]
        static void CreateLevelingUpMenuObject()
        {
            var go = Resources.Load("Prefabs/Menu Objects/EnableLeveling");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }
        #endregion

        #region Props
        [MenuItem("Deceilio/Phaedra/Props/Invisible Wall")]
        static void CreateInvisibleWallObject()
        {
            var go = Resources.Load("Prefabs/Props Objects/InvisibleWall");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }
        #endregion

        #region Testing
        [MenuItem("Deceilio/Phaedra/Testing/Pickup/Weapon")]
        static void CreatePickupWeapon()
        {
            var go = Resources.Load("Prefabs/Item Objects/Pickup/PickupWeapon");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }

        [MenuItem("Deceilio/Phaedra/Testing/Pickup/Item/Vial of Vigour")]
        static void CreatePickupVialOfVigour()
        {
            var go = Resources.Load("Prefabs/Item Objects/Pickup/PickupVialOfVigour");
            Instantiate(go, Vector3.zero, Quaternion.identity);
        }
        #endregion
    }
}