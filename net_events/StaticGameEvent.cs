using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Steamroller.Service.Core.GameEvents
{
    public abstract class StaticGameEvent<T>
    {
        [ NonSerialized ]
        public static List<UnityAction> listeners = new List<UnityAction>();

        public StaticGameEvent()
        {
            throw( new Exception( "No instance of StaticGameEvent should be created" ) );
        }

        public static void Invoke()
        {
            foreach( var _listener in listeners )
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
        }

        public static void AddGlobalListener( UnityAction listener )
        {
            if( listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Add( listener );
        }

        public static void RemoveGlobalListener( UnityAction listener )
        {
            if( !listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Remove( listener );
        }
    }
}
