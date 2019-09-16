/*
 Author: Aaron Hines
 Edits By: 
 Description: Extends Unity Events to have global listeners
 */
using System;
using UnityEngine.Events;

namespace GameFramework.Events
{
    [Serializable]
    public class GameEvent<T> : UnityEvent // T is the source
    {
        public GameEvent(T source) : base()
        {
            this.source = source;
        }

        private T source;

        private static UnityAction<T> globalAction;

        public static void AddGlobalListener(UnityAction<T> listener)
        {
            globalAction += listener;
        }

        public static void RemoveGlobalListener(UnityAction<T> listener)
        {
            globalAction -= listener;
        }

        public new void Invoke()
        {
            base.Invoke(); // invoke the normal event
            globalAction?.Invoke(source); // invoke the static one
        }


        public void InvokeGlobal()
        {
            globalAction?.Invoke( source );
        }
    }
}
