using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace Steamroller.Service.Core.GameEvents
{
    public abstract class GameEvent<T> : UnityEvent
    {
        [ NonSerialized ]
        public static List<UnityAction> listeners = new List<UnityAction>();

        [ NonSerialized ]
        public static Stack<object> sources = new Stack<object>();

        [ NonSerialized ]
        public object source;

        public GameEvent ()
        {
        }

        public GameEvent ( object source )
        {
            this.source = source;
        }

        public static object GetSource ()
        {
            return sources.LastOrDefault();
        }

        public new void Invoke ()
        {
            sources.Push( source );

            try
            {
                base.Invoke();
            }
            catch( Exception exception )
            {
                Debug.LogException( exception );
            }

            foreach( var _listener in listeners.ToList() )
            {
                try
                {
                    _listener();
                }
                catch( Exception exception )
                {
                    Debug.LogException( exception );
                }
            }

            sources.Pop();
        }

        public static void AddGlobalListener ( UnityAction listener )
        {
            if( listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Add( listener );
        }

        public static void RemoveGlobalListener ( UnityAction listener )
        {
            if( !listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Remove( listener );
        }

        public void AddPersistentListener ( UnityAction listener )
        {
#if UNITY_EDITOR
            RemovePersistentListener( listener );

            UnityEventTools.AddPersistentListener( this, listener );
#endif
        }

        public void AddBoolPersistentListener ( UnityAction<bool> listener, bool argument )
        {
#if UNITY_EDITOR
            RemovePersistentListener<bool>( listener );

            UnityEventTools.AddBoolPersistentListener( this, listener, argument );
#endif
        }

        public void AddFloatPersistentListener ( UnityAction<float> listener, float argument )
        {
#if UNITY_EDITOR
            RemovePersistentListener<float>( listener );

            UnityEventTools.AddFloatPersistentListener( this, listener, argument );
#endif
        }

        public void AddIntPersistentListener ( UnityAction<int> listener, int argument )
        {
#if UNITY_EDITOR
            RemovePersistentListener<int>( listener );

            UnityEventTools.AddIntPersistentListener( this, listener, argument );
#endif
        }

        public void AddObjectPersistentListener<T0>( UnityAction<T0> listener, T0 argument ) where T0 : UnityEngine.Object
        {
#if UNITY_EDITOR
            RemovePersistentListener<T0>( listener );

            UnityEventTools.AddObjectPersistentListener<T0>( this, listener, argument );
#endif
        }

        public void AddStringPersistentListener ( UnityAction<string> listener, string argument )
        {
#if UNITY_EDITOR
            RemovePersistentListener<string>( listener );

            UnityEventTools.AddStringPersistentListener( this, listener, argument );
#endif
        }

        public void RemovePersistentListener ( UnityAction listener )
        {
#if UNITY_EDITOR
            for( var _index = 0; _index < GetPersistentEventCount(); _index++ )
            {
                var _target = GetPersistentTarget( _index );
                var _methodName = GetPersistentMethodName( _index );
                if( ( UnityEngine.Object )listener.Target == _target && listener.Method.Name == _methodName )
                {
                    UnityEventTools.RemovePersistentListener( this, listener );
                }
            }
#endif
        }

        public void RemovePersistentListener<T0>( UnityAction<T0> listener )
        {
#if UNITY_EDITOR
            for( var _index = 0; _index < GetPersistentEventCount(); _index++ )
            {
                var _target = GetPersistentTarget( _index );
                var _methodName = GetPersistentMethodName( _index );
                if( ( UnityEngine.Object )listener.Target == _target && listener.Method.Name == _methodName )
                {
                    UnityEventTools.RemovePersistentListener<T0>( this, listener );
                }
            }
#endif
        }
    }
}
