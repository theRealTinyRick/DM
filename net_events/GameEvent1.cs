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
    public abstract class GameEvent<TEventType, TEventArg> : UnityEvent<TEventArg>
    {
        public static List<UnityAction<TEventArg>> listeners = new List<UnityAction<TEventArg>>();

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

        public new void Invoke( TEventArg argument1 )
        {
            sources.Push( source );

            try
            {
                base.Invoke( argument1 );
            }
            catch( Exception exception )
            {
                Debug.LogException( exception );
            }

            foreach( var _listener in listeners.ToList() )
            {
                try
                {
                    _listener( argument1 );
                }
                catch( Exception exception )
                {
                    Debug.LogException( exception );
                }
            }

            sources.Pop();
        }

        public static void AddGlobalListener( UnityAction<TEventArg> listener )
        {
            if( listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Add( listener );
        }

        public static void RemoveGlobalListener( UnityAction<TEventArg> listener )
        {
            if( !listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Remove( listener );
        }

        /// <summary>
        /// Assigns the method that you pass in as a listener for the event. Similar to: event += listener.
        /// </summary>
        /// <param name="listener">Method to be called when the event is fired.</param>
        public void AddPersistentListener( UnityAction<TEventArg> listener )
        {
#if UNITY_EDITOR
            RemovePersistentListener( listener );

            UnityEventTools.AddPersistentListener<TEventArg>( this, listener );
#endif
        }

        /// <summary>
        /// The mmethod that you pass will be removed as a listener for the event. Similar to: event -= listener.
        /// </summary>
        /// <param name="listener">Method to be removed from the event's listeners.</param>
        public void RemovePersistentListener( UnityAction<TEventArg> listener )
        {
#if UNITY_EDITOR
            for( var _index = 0; _index < GetPersistentEventCount(); _index++ )
            {
                var _target = GetPersistentTarget( _index );
                var _methodName = GetPersistentMethodName( _index );
                if( ( UnityEngine.Object )listener.Target == _target && listener.Method.Name == _methodName )
                {
                    UnityEventTools.RemovePersistentListener<TEventArg>( this, listener );
                }
            }
#endif
        }
    }
}
