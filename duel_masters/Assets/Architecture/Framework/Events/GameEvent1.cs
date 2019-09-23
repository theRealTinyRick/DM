/*
 Author: Aaron Hines
 Edits By: 
 Description: Extends Unity Events to have global listeners with one param
 */
using System;
using UnityEngine.Events;

namespace GameFramework.Events
{
    [Serializable]
    public class GameEvent<T1, T2> : UnityEvent<T2> // T is the source
    {
        public GameEvent(T1 source) : base()
        {
            this.source = source;
        }

        private T1 source;

        private static UnityAction<T1, T2> globalAction;

        public static void AddGlobalListener(UnityAction<T1, T2> listener)
        {
            globalAction += listener;
        }

        public static void RemoveGlobalListener(UnityAction<T1, T2> listener)
        {
            globalAction -= listener;
        }

        public new void Invoke(T2 arg)
        {
            base.Invoke(arg);
            globalAction?.Invoke(source, arg);
        }

        public static void InvokeGlobal(T1 source, T2 arg )
        {
            globalAction?.Invoke(source, arg);
        }
    }
}
