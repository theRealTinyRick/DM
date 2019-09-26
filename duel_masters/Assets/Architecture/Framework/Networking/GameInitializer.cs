using UnityEngine;

namespace GameFramework.Networking
{
    public class GameInitializer : MonoBehaviour
    {
        void Start()
        {
            NetworkManager.instance.OnLevelInitialized();
        }
    }
}
