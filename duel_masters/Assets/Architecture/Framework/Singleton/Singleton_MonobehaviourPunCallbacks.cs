/*
 Author: Aaron Hines
 Edits By: 
 Description: Creates a singleton with pun callbacks
 */

using UnityEngine;
using Photon.Pun;

public abstract class Singleton_MonobehaviourPunCallbacks<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
{
    private static T _instance;

    private static object _lock = new object();

    public static T instance
    {
        get
        {
            lock ( _lock )
            {
                if ( _instance == null )
                {
                    _instance = (T)FindObjectOfType( typeof( T ) );

                    if ( FindObjectsOfType( typeof( T ) ).Length > 1 )
                    {
                        return _instance;
                    }

                    if ( _instance == null )
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof( T ).ToString();
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    private new void OnEnable()
    {
        base.OnEnable();

        GameObject.DontDestroyOnLoad( gameObject );
        Enable();
    }

    private new  void OnDisable()
    {
        base.OnDisable();

        Disable();
    }

    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }

    // this method is meant to be overridden
    protected virtual void Enable() { }
    // this method is meant to be overridden
    protected virtual void Disable() { }
}
