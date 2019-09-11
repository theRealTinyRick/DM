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
        public GameEvent(T1 source) : base()
        {
            this.source = source;
        }

        private T1 source;

        private static UnityAction<T1, T2, T3> globalAction;

        public static void AddGlobalListener(UnityAction<T1, T2, T3> listener)
        {
            globalAction += listener;
        }

        public static void RemoveGlobalListener(UnityAction<T1, T2, T3> listener)
        {
            globalAction -= listener;
        }

        public new void Invoke(T2 arg1, T3 arg2)
        {
            base.Invoke(arg1, arg2);
            globalAction?.Invoke(source, arg1, arg2);
        }
    }
}
