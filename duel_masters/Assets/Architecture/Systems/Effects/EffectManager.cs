using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;
using DuelMasters.Systems.Gameplay.Locations;

namespace DuelMasters.Systems.Effects
{
    public class EffectManager : Singleton_MonoBehaviour<EffectManager>
    {
        private PhotonView photonView;

        private void OnEnable()
        {
            if (photonView == null)
            {
                photonView = GetComponentInChildren<PhotonView>();
            }
        }

        public Stack<Effect> effectStack = new Stack<Effect>();

        public void StartInstance()
        {
            effectStack.Clear();
        }

        public void StartResolving()
        {
            
        }

        private IEnumerator Resolve()
        {
            while(effectStack.Count > 0)
            {
                Effect _current = effectStack.Peek();
                _current.StartEffect();

                while (!_current.resolved)
                {
                    yield return new WaitForEndOfFrame();
                }
            }

            yield break;
        }

        public void PlaceOnStack(Effect effect)
        {

        }

        public void RemoveFromStack(Effect effect)
        {

        }
    }
}
