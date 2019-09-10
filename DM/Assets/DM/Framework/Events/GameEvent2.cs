/*
 Author: Aaron Hines
 Edits By: 
 Description: Extends Unity Events to have global listeners with two params
 */
using System;
using UnityEngine.Events;

namespace Assets.FuzzyCarnival.GameFramework.Events
{
    [Serializable]
    public class GameEvent<T1, T2, T3> : UnityEvent<T2, T3> // T is the source
    {
        private static UnityAction<T1, T2, T3> globalAction;

        public void AddGlobalListener(UnityAction<T1, T2, T3> listener)
        {
            globalAction += listener;
        }

        public void RemoveGlobalListener(UnityAction<T1, T2, T3> listener)
        {
            globalAction -= listener;
        }

        public void Invoke(T1 source, T2 arg1, T3 arg2)
        {
            Invoke(arg1, arg2);
            globalAction?.Invoke(source, arg1, arg2);
        }
    }
}
