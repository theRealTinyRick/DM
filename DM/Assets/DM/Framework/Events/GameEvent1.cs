/*
 Author: Aaron Hines
 Edits By: 
 Description: Extends Unity Events to have global listeners with one param
 */
using System;
using UnityEngine.Events;

namespace Assets.FuzzyCarnival.GameFramework.Events
{
    [Serializable]
    public class GameEvent<T1, T2> : UnityEvent<T2> // T is the source
    {
        private static UnityAction<T1, T2> globalAction;

        public void AddGlobalListener(UnityAction<T1, T2> listener)
        {
            globalAction += listener;
        }

        public void RemoveGlobalListener(UnityAction<T1, T2> listener)
        {
            globalAction -= listener;
        }

        public void Invoke(T1 source, T2 arg)
        {
            Invoke(arg);
            globalAction?.Invoke(source, arg);
        }
    }
}
