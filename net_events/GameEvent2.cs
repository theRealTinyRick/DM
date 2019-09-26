using System;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace Steamroller.Service.Core.GameEvents
{
    public abstract class GameEvent<T, T1, T2> : UnityEvent<T1, T2>
    {
        public static List<UnityAction<T1, T2>> listeners = new  List<UnityAction<T1, T2>>();

        [ NonSerialized ]
        public static Stack<object> sources = new Stack<object>();

        [ NonSerialized ]
        public object source;

        public GameEvent()
        {
        }

        public GameEvent( object source )
        {
            this.source = source;
        }

        public static object GetSource()
        {
            return sources.LastOrDefault();
        }

        public new void Invoke( T1 argument1, T2 argument2 )
        {
            sources.Push( source );

            try
            {
                base.Invoke( argument1, argument2 );
            }
            catch( Exception exception )
            {
                Debug.LogException( exception );
            }

            foreach( var _listener in listeners.ToList() )
            {
                try
                {
                    _listener( argument1, argument2 );
                }
                catch( Exception exception )
                {
                    Debug.LogException( exception );
                }
            }

            sources.Pop();
        }

        public static void AddGlobalListener( UnityAction<T1, T2> listener )
        {
            if( listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Add( listener );
        }

        public static void RemoveGlobalListener( UnityAction<T1, T2> listener )
        {
            if( !listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Remove( listener );
        }

        public void AddPersistentListener( UnityAction<T1, T2> listener )
        {
#if UNITY_EDITOR
            RemovePersistentListener( listener );

            UnityEventTools.AddPersistentListener<T1, T2>( this, listener );
#endif
        }

        public void RemovePersistentListener( UnityAction<T1, T2> listener )
        {
#if UNITY_EDITOR
            for( var _index = 0; _index < GetPersistentEventCount(); _index++ )
            {
                var _target = GetPersistentTarget( _index );
                var _methodName = GetPersistentMethodName( _index );
                if( ( UnityEngine.Object )listener.Target == _target && listener.Method.Name == _methodName )
                {
                    UnityEventTools.RemovePersistentListener<T1, T2>( this, listener );
                }
            }
#endif
        }
    }
}
