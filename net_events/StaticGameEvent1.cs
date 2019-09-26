using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Steamroller.Service.Core.GameEvents
{
    public abstract class StaticGameEvent<T, T1>
    {
        [ NonSerialized ]
        public static List<UnityAction<T1>> listeners = new List<UnityAction<T1>>();

        public StaticGameEvent()
        {
            throw( new Exception( "No instance of StaticGameEvent should be created" ) );
        }

        public static void Invoke( T1 argument1 )
        {
            foreach( var _listener in listeners )
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
        }

        public static void AddGlobalListener( UnityAction<T1> listener )
        {
            if( listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Add( listener );
        }

        public static void RemoveGlobalListener( UnityAction<T1> listener )
        {
            if( !listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Remove( listener );
        }
    }
}
