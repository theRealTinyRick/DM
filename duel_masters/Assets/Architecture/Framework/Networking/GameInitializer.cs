using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

namespace GameFramework.Netorking.Photon
{
    public class GameInitializer : MonoBehaviourPunCallbacks
    {
        void Start()
        {
            RegisterLevelLoaded();
        }

        private void RegisterLevelLoaded()
        {

        }
    }
}
