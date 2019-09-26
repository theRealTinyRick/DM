﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Steamroller.Service.Core.GameEvents
{
    public abstract class StaticGameEvent<T, T1, T2, T3>
    {
        [ NonSerialized ]
        public static List<UnityAction<T1, T2, T3>> listeners = new List<UnityAction<T1, T2, T3>>();

        public StaticGameEvent()
        {
            throw( new Exception( "No instance of StaticGameEvent should be created" ) );
        }

        public static void Invoke( T1 argument1, T2 argument2, T3 argument3 )
        {
            foreach( var _listener in listeners )
            {
                try
                {
                    _listener( argument1, argument2, argument3 );
                }
                catch( Exception exception )
                {
                    Debug.LogException( exception );
                }
            }
        }

        public static void AddGlobalListener( UnityAction<T1, T2, T3> listener )
        {
            if( listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Add( listener );
        }

        public static void RemoveGlobalListener( UnityAction<T1, T2, T3> listener )
        {
            if( !listeners.Contains( listener ) )
            {
                return;
            }

            listeners.Remove( listener );
        }
    }
}
